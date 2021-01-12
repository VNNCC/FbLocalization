using System.Diagnostics;
using FbLocalization.Controls;

namespace FbLocalization.Windows
{
    public partial class AboutDialog : CustomWindow
    {
        public AboutDialog()
        {
            this.InitializeComponent();

            this.licenseTextBox.Text = LicenseInformation.LicenseText;
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri)
            {
                UseShellExecute = true
            });
        }
    }
}