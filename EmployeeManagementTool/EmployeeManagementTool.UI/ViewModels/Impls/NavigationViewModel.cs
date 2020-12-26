using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.DataModel;
using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IEmployeeAccessor _employeeAccessor;
        public ObservableCollection<Employee> Employees { get; set; }

        public NavigationViewModel(IEmployeeAccessor employeeAccessor)
        {
            _employeeAccessor = employeeAccessor;
            Employees = new ObservableCollection<Employee>();
        }

        public async Task LoadAsync()
        {
            var employees = await _employeeAccessor.GetAllEmployeesAsync();
            foreach (var employee in employees)
            {
                Employees.Add(employee);
            }
        }
    }
}
