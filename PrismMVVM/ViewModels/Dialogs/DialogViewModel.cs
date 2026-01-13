using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Windows.Input;

namespace PrismMVVM.ViewModels.Dialogs
{
    /// <summary>
    /// DialogViewModel demonstrates Prism Dialog Service implementation:
    /// - Implements IDialogAware interface for dialog lifecycle management
    /// - Receives parameters when dialog opens
    /// - Returns results and parameters when dialog closes
    /// - Uses RequestClose event to notify Dialog Service
    /// </summary>
    public class DialogViewModel : BindableBase, IDialogAware
    {
        private string _message;

        public DialogViewModel()
        {
            ConfirmCommand = new DelegateCommand(Confirm, () => true);
            CancelCommand = new DelegateCommand(() => CloseDialog(ButtonResult.Cancel));
        }

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        // Dialog title displayed in the dialog window
        public string Title => "Prism MVVM Dialog";

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        // Event that notifies Prism Dialog Service when dialog should close
        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// Called by Dialog Service to determine if dialog can be closed
        /// Return false to prevent dialog from closing
        /// </summary>
        public bool CanCloseDialog()
        {
            return true;
        }

        /// <summary>
        /// Called after dialog is closed - cleanup logic goes here
        /// </summary>
        public void OnDialogClosed()
        {
            // Do nothing
        }

        /// <summary>
        /// Called when dialog is opened - receive parameters from the caller
        /// </summary>
        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters?.GetValue<string>("Message");
        }

        private void Confirm()
        {
            CloseDialog(ButtonResult.OK);
        }

        /// <summary>
        /// Closes dialog with result and optional return parameters
        /// </summary>
        private void CloseDialog(ButtonResult result)
        {
            IDialogParameters returnDialogParams = null;
            if (result == ButtonResult.OK)
            {
                // Return parameters back to the caller
                returnDialogParams = new DialogParameters
                {
                    { "ConfirmMessage", "Dialog has been opened" }
                };
            }

            RaiseRequestClose(new DialogResult(result, returnDialogParams));
        }

        /// <summary>
        /// Raises RequestClose event to notify Dialog Service
        /// </summary>
        private void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
            OnDialogClosed();
        }
    }
}
