using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using PrismMVVM.Views;
using System.Windows;
using System.Windows.Input;

namespace PrismMVVM.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IContainerProvider _containerProvider;

        private string _userName;
        private string _password;

        public LoginViewModel(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            LoginCommand = new DelegateCommand(Login, CanLogin)
                .ObservesProperty(() => UserName)
                .ObservesProperty(() => Password);
            QuitCommand = new DelegateCommand(Quit, () => true);
        }

        public ICommand LoginCommand { get; }
        public ICommand QuitCommand { get; }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private void Login()
        {
            _containerProvider.Resolve<MainView>().ShowDialog();
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        }

        private void Quit()
        {
            Application.Current.Shutdown();
        }
    }
}
