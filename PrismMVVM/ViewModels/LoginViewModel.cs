using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using PrismMVVM.Views;
using System.Windows;
using System.Windows.Input;

namespace PrismMVVM.ViewModels
{
    /// <summary>
    /// LoginViewModel demonstrates:
    /// - DelegateCommand with CanExecute validation
    /// - ObservesProperty for automatic command refresh
    /// - Simple navigation pattern (educational simplification)
    /// </summary>
    public class LoginViewModel : BindableBase
    {
        private readonly IContainerProvider _containerProvider;

        private string _userName;
        private string _password;

        public LoginViewModel(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            // DelegateCommand with CanExecute that automatically refreshes when properties change
            LoginCommand = new DelegateCommand(Login, CanLogin)
                .ObservesProperty(() => UserName)
                .ObservesProperty(() => Password);
                
            QuitCommand = new DelegateCommand(Quit, () => true);
        }

        public ICommand LoginCommand { get; }
        public ICommand QuitCommand { get; }

        // Note: In production, use PasswordBox with secure binding instead of TextBox
        // This is simplified for educational purposes to demonstrate property binding
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

        /// <summary>
        /// Simplified navigation approach for educational purposes
        /// Production apps should use: Prism Region Navigation, IWindowManager, or navigation service abstraction
        /// </summary>
        private void Login()
        {
            _containerProvider.Resolve<MainView>().ShowDialog();
        }

        /// <summary>
        /// CanExecute method automatically called when observed properties change
        /// Enables/disables the LoginCommand button
        /// </summary>
        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        }

        private void Quit()
        {
            // Note: In production, use an application service abstraction for testability
            Application.Current.Shutdown();
        }
    }
}
