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
    }
}