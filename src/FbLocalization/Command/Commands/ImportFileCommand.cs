using System.IO;
using System.Windows;
using FbLocalization.Core.IO.Reader;
using FbLocalization.ViewModel;
using Microsoft.Win32;

namespace FbLocalization.Command.Commands
{
    /// <summary>
    /// Import binary chunk file
    /// </summary>
    public sealed class ImportFileCommand : BaseCommand
    {
        /// <summary>
        /// Defines if any file is loaded
        /// </summary>
        public static bool FileLoaded;

        public static readonly ImportFileCommand Instance = new ImportFileCommand();

        /// <summary>
        /// Import binary chunk file
        /// </summary>
        /// <param name="parameter">current application view model (<see cref="AppVM"/>)</param>
        public override void Execute(object? parameter)
        {
            if (parameter is AppVM appVM)
            {
                var of = new OpenFileDialog
                {
                    Filter = "Binary Chunk|*.chunk"
                };

                if (of.ShowDialog() == true)
                {
                    try
                    {
                        using var reader = new LocalizationReader(new FileStream(of.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                        appVM.SetEntriesFrom(reader.ReadEntries());

                        FileLoaded = true;
                        ExportFileCommand.Instance.FireExecutionChanged();
                    }
                    catch
                    {
                        MessageBox.Show("Could not read file: " + of.FileName);
                    }
                }
            }
        }
    }
}