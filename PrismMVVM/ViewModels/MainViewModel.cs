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
    public class MainViewModel : BindableBase
    {
        private readonly IContainerProvider _containerProvider;
        private readonly IEmployeeService _employeeService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;

        private string _dialogResult;

        public MainViewModel(IContainerProvider containerProvider,
                             IEmployeeService employeeService,
                             IEventAggregator eventAggregator,
                             IDialogService dialogService)
        {
            _containerProvider = containerProvider;
            _employeeService = employeeService;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;

            Employees = new ObservableCollection<Employee>(_employeeService.GetAllEmployees());
            SumView = _containerProvider.Resolve<SumView>();

            DialogResult = "Dialog not opened yet";

            SendEventCommand = new DelegateCommand(SendEvent);
            ShowDialogCommand = new DelegateCommand(ShowDialog);
        }

        public ICommand SendEventCommand { get; }
        public ICommand ShowDialogCommand { get; }

        public ObservableCollection<Employee> Employees { get; }

        public UserControl SumView { get; }

        public string DialogResult
        {
            get => _dialogResult;
            set => SetProperty(ref _dialogResult, value);
        }

        private void SendEvent()
        {
            _eventAggregator.GetEvent<EmployeeEvent>().Publish(Employees);
        }

        private void ShowDialog()
        {
            IDialogParameters parameters = new DialogParameters
            {
                { "Message", "Message from MainViewModel" }
            };
            _dialogService.ShowDialog(nameof(DialogView), parameters, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    DialogResult = result.Parameters?.GetValue<string>("ConfirmMessage");
                }
            });
        }
    }
}
