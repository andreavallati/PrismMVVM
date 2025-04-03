using Prism.Events;
using Prism.Mvvm;
using PrismMVVM.EventAggregator;
using PrismMVVM.Models;
using PrismMVVM.ViewModels.Interfaces;

namespace PrismMVVM.ViewModels
{
    public class SumViewModel : BindableBase, ISumViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        private string _fullNames;
        private int _ageSum;
        private double _salarySum;

        public SumViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

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

        private void OnEmployeeEvent(IEnumerable<Employee> employees)
        {
            FullNames = string.Join(" - ", employees.Select(e => $"{e.FirstName} {e.LastName}"));
            AgeSum = employees.Sum(e => e.Age);
            SalarySum = employees.Sum(e => e.Salary);
        }
    }
}
