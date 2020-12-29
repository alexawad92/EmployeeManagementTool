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
        private readonly IDetailViewModelSavedEvent _detailViewModelSavedEvent;
        public ObservableCollection<NavigationItemViewModel> Employees { get; set; }

    
        public NavigationViewModel(IEmployeeAccessor employeeAccessor, INavigationSelectionChangedEvent navigationSelectionChangedEvent, IDetailViewModelSavedEvent detailViewModelSavedEvent)
        {
            _employeeAccessor = employeeAccessor;
            _navigationSelectionChangedEvent = navigationSelectionChangedEvent;
            _detailViewModelSavedEvent = detailViewModelSavedEvent;
            _detailViewModelSavedEvent.OnDetailViewModelSaved += _detailViewModelSavedEvent_OnOnDetailViewModelSaved;
            Employees = new ObservableCollection<NavigationItemViewModel>();
        }

        private async void _detailViewModelSavedEvent_OnOnDetailViewModelSaved(object sender, int id)
        {
            var outdatedNavigationItemViewModel = Employees.Single(navigationItem => navigationItem.Id == id);
            await _employeeAccessor.ReloadEmployeeAsync(id);
            var updatedEmployee = await _employeeAccessor.GetEmployeeByIdAsync(id);
            outdatedNavigationItemViewModel.DisplayMember = $"{updatedEmployee.FirstName} {updatedEmployee.LastName}";
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
