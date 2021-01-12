using System;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace FbLocalization.LicenseGenerator
{
    [Generator]
    public class LicenseGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            string licenseFilePath = null;
            foreach (var file in context.AdditionalFiles)
                if (file.Path.EndsWith("license", StringComparison.OrdinalIgnoreCase))
                    licenseFilePath = file.Path;

            if (licenseFilePath == null)
                throw new Exception("File path not found!");

            var source = new StringBuilder(@"

namespace FbLocalization
{
    public static class LicenseInformation
    {
        public const string LicenseText = @""" + File.ReadAllText(licenseFilePath).Replace("\"", "\"\"") + @""";
    }
}

            ");

            // Inject the created source into the users compilation
            context.AddSource("licenseGenerator.cs", SourceText.From(source.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}