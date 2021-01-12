using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FbLocalization.Core.ViewModel
{
    /// <summary>
    /// Base view model for firing property changes
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Fire the <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="propertyName">name of the property that has changed</param>
        public void FireProperty([CallerMemberName] string propertyName = "")
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}