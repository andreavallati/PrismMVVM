using Prism.Events;
using PrismMVVM.Models;

namespace PrismMVVM.EventAggregator
{
    public class EmployeeEvent : PubSubEvent<IEnumerable<Employee>>
    {

    }
}
