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
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<LoginView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register Services
            containerRegistry.RegisterSingleton<IEmployeeService, EmployeeService>();

            // Register Types
            containerRegistry.Register<ISumViewModel, SumViewModel>();

            // Register Dialogs
            containerRegistry.RegisterDialog<DialogView, DialogViewModel>();
        }
    }
}
