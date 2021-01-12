using System.Collections.Generic;
using System.IO;
using System.Windows;
using FbLocalization.Core.IO.Writer;
using FbLocalization.ViewModel;
using Microsoft.Win32;

namespace FbLocalization.Command.Commands
{
    /// <summary>
    /// Export localization
    /// </summary>
    public class ExportFileCommand : BaseCommand
    {
        public static readonly ExportFileCommand Instance = new ExportFileCommand();

        /// <summary>
        /// Export localization file
        /// </summary>
        /// <param name="parameter">current application view model (<see cref="AppVM"/>)</param>
        public override void Execute(object? parameter)
        {
            if (parameter is AppVM appVM)
            {
                string[] supportedFileFormats =
                {
                    "Binary Chunk|*.chunk",
                    "CSV File|*.csv"
                };

                var of = new SaveFileDialog
                {
                    Filter = string.Join('|', supportedFileFormats)
                };

                if (of.ShowDialog() == true)
                {
                    switch (of.FilterIndex)
                    {
                        // Binary chunk
                        case 1:
                            {
                                using var writer = new LocalizationWriter(new FileStream(of.FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite));
                                writer.Write(appVM.EntriesNoFilter);
                                break;
                            }

                        // CSV
                        case 2:
                            {
                                using var tw = new StreamWriter(new FileStream(of.FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite));
                                foreach (var entry in appVM.EntriesNoFilter)
                                    tw.WriteLine($"\"{entry.HashString}\",\"{entry.Value}\"");

                                break;
                            }
                    }

                    MessageBox.Show("Saved!");
                }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool CanExecute(object? parameter) => ImportFileCommand.FileLoaded;
    }
}