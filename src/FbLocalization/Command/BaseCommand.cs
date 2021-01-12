using System;
using System.Windows.Input;

namespace FbLocalization.Command
{
    /// <summary>
    /// Base command class
    /// </summary>
    public class BaseCommand : ICommand
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        private Action<object?>? m_ExecuteAction;
        private Predicate<object?>? m_CanExecuteAction;

        /// <summary>
        /// Empty command
        /// </summary>
        public BaseCommand()
        {
        }

        /// <summary>
        /// Create new command with actions
        /// </summary>
        /// <param name="execute">execute action</param>
        /// <param name="canExecute">can execute action</param>
        public BaseCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            this.m_ExecuteAction = execute ?? throw new ArgumentNullException(nameof(execute));
            this.m_CanExecuteAction = canExecute;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual bool CanExecute(object? parameter) => this.m_CanExecuteAction == null || this.m_CanExecuteAction(parameter);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void Execute(object? parameter)
        {
            if (this.m_ExecuteAction != null)
                this.m_ExecuteAction.Invoke(parameter);
            else
                throw new NotImplementedException();
        }

        /// <summary>
        /// Fire <see cref="ICommand.CanExecuteChanged"/> event
        /// </summary>
        public void FireExecutionChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}