using FbLocalization.Controls;
using FbLocalization.Core.ViewModel.Models;

namespace FbLocalization.Windows
{
    /// <summary>
    /// Window to change the <see cref="LocalizationEntryVM.Value"/>
    /// </summary>
    public partial class EditEntryWindow : CustomWindow
    {
        public EditEntryWindow(LocalizationEntryVM entry)
        {
            this.InitializeComponent();
            this.Title = "Edit: " + entry.HashString;
            this.DataContext = entry;
        }
    }
}