namespace FbLocalization.Core.ViewModel.Models
{
    /// <summary>
    /// Localization item
    /// </summary>
    public sealed class LocalizationEntryVM : BaseViewModel
    {
        /// <summary>
        /// Item value
        /// </summary>
        private string m_Value;

        /// <summary>
        /// Item hash
        /// </summary>
        private uint m_Hash;

        /// <summary>
        /// Localization entry
        /// </summary>
        /// <param name="hash">entry hash</param>
        /// <param name="value">entry value</param>
        public LocalizationEntryVM(uint hash, string value)
        {
            this.m_Value = value;
            this.m_Hash = hash;
        }

        /// <summary>
        /// Item value
        /// </summary>
        public string Value
        {
            get => this.m_Value;

            set
            {
                if (value == this.m_Value)
                    return;

                this.m_Value = value;
                this.FireProperty();
            }
        }

        /// <summary>
        /// Item hash
        /// </summary>
        public uint Hash
        {
            get => this.m_Hash;

            set
            {
                if (value == this.m_Hash)
                    return;

                this.m_Hash = value;
                this.FireProperty();
                this.FireProperty(nameof(this.HashString));
            }
        }

        /// <summary>
        /// <see cref="Hash"/> as string format
        /// </summary>
        public string HashString => this.m_Hash.ToString("X8");
    }
}