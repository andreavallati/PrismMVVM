using Prism.DryIoc;
using Prism.Ioc;
using PrismMVVM.Services;
using PrismMVVM.ViewModels;
using PrismMVVM.ViewModels.Dialogs;
using PrismMVVM.ViewModels.Interfaces;
using PrismMVVM.Views;
using PrismMVVM.Views.Dialogs;
using System.Windows;

namespace PrismMVVM
{
    /// <summary>
    /// Prism Application bootstrap class
    /// Extends PrismApplication to configure DI container and application startup
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <summary>
        /// Creates the main/shell window of the application
        /// Called automatically by Prism during startup
        /// </summary>
        protected override Window CreateShell()
        {
            // Return the initial window - LoginView in this case
            return Container.Resolve<LoginView>();
        }

        /// <summary>
        /// Register all services, ViewModels, and dialogs with the DI container
        /// Called automatically by Prism before CreateShell
        /// </summary>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register Services - Singleton ensures one instance throughout app lifetime
            containerRegistry.RegisterSingleton<IEmployeeService, EmployeeService>();

            // Register Types - Creates new instance each time resolved
            // ISumViewModel registered to enable interface-based dependency injection
            containerRegistry.Register<ISumViewModel, SumViewModel>();

            // Register Dialogs - Special registration for Prism Dialog Service
            // Associates DialogView with DialogViewModel and enables ShowDialog
            containerRegistry.RegisterDialog<DialogView, DialogViewModel>();
        }
    }
}
