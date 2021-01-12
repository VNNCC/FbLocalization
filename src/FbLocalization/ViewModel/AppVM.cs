using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FbLocalization.Command;
using FbLocalization.Core.ViewModel;
using FbLocalization.Core.ViewModel.Models;
using FbLocalization.Windows;

namespace FbLocalization.ViewModel
{
    /// <summary>
    /// Main application view model
    /// </summary>
    public sealed class AppVM : BaseViewModel
    {
        private ObservableCollection<LocalizationEntryVM> m_Entries = new ObservableCollection<LocalizationEntryVM>();
        private string m_FilterText = "";

        public AppVM()
        {
            this.FilterCommand = new((obj) =>
            {
                this.FireFilterProperties();
            });
        }

        /// <summary>
        /// Fire all properties that are related to the <see cref="FilterText"/>
        /// </summary>
        private void FireFilterProperties()
        {
            this.FireProperty();
            this.FireProperty(nameof(this.Entries));
        }

        /// <summary>
        /// Set localization entries
        /// </summary>
        /// <param name="entries">new entries</param>
        public void SetEntriesFrom(IEnumerable<LocalizationEntryVM> entries) => this.Entries = new ObservableCollection<LocalizationEntryVM>(entries);

        /// <summary>
        /// Localization filter
        /// </summary>
        public string FilterText
        {
            get => this.m_FilterText;

            set
            {
                if (this.m_FilterText == value)
                    return;

                this.m_FilterText = value;
                this.FireFilterProperties();
            }
        }

        /// <summary>
        /// Localization entries without filter applied
        /// </summary>
        public ObservableCollection<LocalizationEntryVM> EntriesNoFilter => this.m_Entries;

        /// <summary>
        /// Localization entries
        /// </summary>
        public ObservableCollection<LocalizationEntryVM> Entries
        {
            get
            {
                if (!string.IsNullOrEmpty(this.FilterText))
                    return new ObservableCollection<LocalizationEntryVM>(this.m_Entries.Where(x => x.Value.ToLower().Contains(this.FilterText.ToLower())));

                return this.m_Entries;
            }

            set
            {
                if (this.m_Entries == value)
                    return;

                this.m_Entries = value;
                this.FireProperty();
                this.FireProperty(nameof(this.EntriesNoFilter));
            }
        }

        /// <summary>
        /// Filter command
        /// </summary>
        public BaseCommand FilterCommand
        {
            get;
        }

        /// <summary>
        /// Show <see cref="AboutDialog"/> window
        /// </summary>
        public BaseCommand ShowAboutDialogCommand
        {
            get;
        } = new((obj) =>
        {
            new AboutDialog().ShowDialog();
        });
    }
}