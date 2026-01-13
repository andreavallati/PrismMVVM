using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismMVVM.EventAggregator;
using PrismMVVM.Models;
using PrismMVVM.Services;
using PrismMVVM.Views;
using PrismMVVM.Views.Dialogs;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrismMVVM.ViewModels
{
    /// <summary>
    /// MainViewModel demonstrates key Prism concepts:
    /// - Dependency Injection: Services and Prism infrastructure injected via constructor
    /// - Event Aggregator: Publish events to loosely-coupled subscribers
    /// - Dialog Service: Show modal dialogs with parameter passing
    /// - Commands: DelegateCommand for user interactions
    /// </summary>
    public class MainViewModel : BindableBase
    {
        // Prism's IContainerProvider allows resolving types from the DI container
        private readonly IContainerProvider _containerProvider;
        
        // Custom service injected via Dependency Injection
        private readonly IEmployeeService _employeeService;
        
        // Prism's Event Aggregator enables loosely-coupled communication between ViewModels
        private readonly IEventAggregator _eventAggregator;
        
        // Prism's Dialog Service manages modal dialogs in an MVVM-friendly way
        private readonly IDialogService _dialogService;

        private string _dialogResult;

        /// <summary>
        /// Constructor Injection: All dependencies are provided by Prism's DI container
        /// </summary>
        public MainViewModel(IContainerProvider containerProvider,
                             IEmployeeService employeeService,
                             IEventAggregator eventAggregator,
                             IDialogService dialogService)
        {
            _containerProvider = containerProvider;
            _employeeService = employeeService;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;

            // Load data from service
            Employees = new ObservableCollection<Employee>(_employeeService.GetAllEmployees());
            
            // Resolve child view - demonstrates view composition (simplified approach)
            SumView = _containerProvider.Resolve<SumView>();

            DialogResult = "Dialog not opened yet";

            // Initialize commands
            SendEventCommand = new DelegateCommand(SendEvent);
            ShowDialogCommand = new DelegateCommand(ShowDialog);
        }

        public ICommand SendEventCommand { get; }
        public ICommand ShowDialogCommand { get; }

        public ObservableCollection<Employee> Employees { get; }

        // Demonstrates view composition - SumView is injected and bound to UI
        public UserControl SumView { get; }

        public string DialogResult
        {
            get => _dialogResult;
            set => SetProperty(ref _dialogResult, value);
        }

        /// <summary>
        /// Publishes an event using Event Aggregator pattern
        /// Any subscriber to EmployeeEvent will receive this data without direct coupling
        /// </summary>
        private void SendEvent()
        {
            // Publish event - loosely coupled communication
            _eventAggregator.GetEvent<EmployeeEvent>().Publish(Employees);
        }

        /// <summary>
        /// Demonstrates Prism Dialog Service usage:
        /// - Pass parameters to dialog
        /// - Handle dialog result with callback
        /// - Retrieve return values from dialog
        /// </summary>
        private void ShowDialog()
        {
            // Create parameters to pass to the dialog
            IDialogParameters parameters = new DialogParameters
            {
                { "Message", "Message from MainViewModel" }
            };
            
            // Show dialog and handle result via callback
            _dialogService.ShowDialog(nameof(DialogView), parameters, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    // Retrieve return value from dialog
                    DialogResult = result.Parameters?.GetValue<string>("ConfirmMessage");
                }
            });
        }
    }
}
