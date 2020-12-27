using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using EmployeeManagementTool.Commands.Impls;
using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.DataModel;
using EmployeeManagementTool.Events.Contracts;
using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IEmployeeAccessor _employeeAccessor;

        private readonly INavigationSelectionChangedEvent _navigationSelectionChangedEvent;
        public ObservableCollection<NavigationItemViewModel> Employees { get; set; }

    
        public NavigationViewModel(IEmployeeAccessor employeeAccessor, INavigationSelectionChangedEvent navigationSelectionChangedEvent)
        {
            _employeeAccessor = employeeAccessor;
            _navigationSelectionChangedEvent = navigationSelectionChangedEvent;
            Employees = new ObservableCollection<NavigationItemViewModel>();
        }

        public async Task LoadAsync()
        {
            var employees = await _employeeAccessor.GetAllEmployeesAsync();
            foreach (var employee in employees)
            {
                string displayMember = $"{employee.FirstName} {employee.LastName}";
                Employees.Add(new NavigationItemViewModel(employee.Id, displayMember, _navigationSelectionChangedEvent));
            }

        }
    }
}
