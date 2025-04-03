using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Windows.Input;

namespace PrismMVVM.ViewModels.Dialogs
{
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

        public string Title => "Prism MVVM Dialog";

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            // Do nothing
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters?.GetValue<string>("Message");
        }

        private void Confirm()
        {
            CloseDialog(ButtonResult.OK);
        }

        private void CloseDialog(ButtonResult result)
        {
            IDialogParameters returnDialogParams = null;
            if (result == ButtonResult.OK)
            {
                returnDialogParams = new DialogParameters
                {
                    { "ConfirmMessage", "Dialog has been opened" }
                };
            }

            RaiseRequestClose(new DialogResult(result, returnDialogParams));
        }

        private void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
            OnDialogClosed();
        }
    }
}
