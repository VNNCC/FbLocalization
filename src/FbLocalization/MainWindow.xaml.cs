using FbLocalization.Controls;
using FbLocalization.ViewModel;

namespace FbLocalization
{
    /// <summary>
    /// Main application window
    /// </summary>
    public partial class MainWindow : CustomWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();

            // Set view model
            this.DataContext = new AppVM();
        }
    }
}