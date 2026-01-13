using Prism.Events;
using Prism.Mvvm;
using PrismMVVM.EventAggregator;
using PrismMVVM.Models;
using PrismMVVM.ViewModels.Interfaces;

namespace PrismMVVM.ViewModels
{
    /// <summary>
    /// SumViewModel demonstrates Event Aggregator pattern:
    /// - Subscribes to EmployeeEvent to receive data from other ViewModels
    /// - Shows loosely-coupled communication without direct references
    /// - Updates UI in response to published events
    /// </summary>
    public class SumViewModel : BindableBase, ISumViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        private string _fullNames;
        private int _ageSum;
        private double _salarySum;

        public SumViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // Subscribe to EmployeeEvent - this ViewModel will receive updates when event is published
            // Note: For production, consider using keepSubscriberReferenceAlive: false for long-lived subscriptions
            _eventAggregator.GetEvent<EmployeeEvent>().Subscribe(OnEmployeeEvent);
        }

        public string FullNames
        {
            get => _fullNames;
            set => SetProperty(ref _fullNames, value);
        }

        public int AgeSum
        {
            get => _ageSum;
            set => SetProperty(ref _ageSum, value);
        }

        public double SalarySum
        {
            get => _salarySum;
            set => SetProperty(ref _salarySum, value);
        }

        /// <summary>
        /// Event handler called when EmployeeEvent is published
        /// Calculates and displays aggregated employee data
        /// </summary>
        private void OnEmployeeEvent(IEnumerable<Employee> employees)
        {
            FullNames = string.Join(" - ", employees.Select(e => $"{e.FirstName} {e.LastName}"));
            AgeSum = employees.Sum(e => e.Age);
            SalarySum = employees.Sum(e => e.Salary);
        }
    }
}
