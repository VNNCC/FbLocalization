using System.Windows;

namespace FbLocalization.Controls
{
    /// <summary>
    /// Custom styled window
    /// </summary>
    public class CustomWindow : Window
    {
        static CustomWindow() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(typeof(CustomWindow)));
    }
}