using FbLocalization.Core.ViewModel.Models;
using FbLocalization.Windows;

namespace FbLocalization.Command.Commands
{
    /// <summary>
    /// Show window to edit the <see cref="LocalizationEntryVM.Value"/>
    /// </summary>
    public class EditEntryCommand : BaseCommand
    {
        public static readonly EditEntryCommand Instance = new EditEntryCommand();

        /// <summary>
        /// Edit <see cref="LocalizationEntryVM.Value"/>
        /// </summary>
        /// <param name="parameter">selected localization entry (<see cref="LocalizationEntryVM"/>)</param>
        public override void Execute(object? parameter)
        {
            if (parameter is LocalizationEntryVM entry)
                new EditEntryWindow(entry).ShowDialog();
        }
    }
}