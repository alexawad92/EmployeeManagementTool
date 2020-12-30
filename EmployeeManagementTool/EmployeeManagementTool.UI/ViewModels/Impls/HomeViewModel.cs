using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using EmployeeManagementTool.Commands.Impls;
using EmployeeManagementTool.Events.Contracts;
using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public class HomeViewModel : ViewModelBase, IMainWindowViewModel
    {
        private readonly IManagementViewModelSelectionChangedEvent _managementViewModelSelectionChangedEvent;
        public ICommand EmployeesManagementCommand { get; set; }
        public ICommand TeamsManagementCommand { get; set; }

        public HomeViewModel(IManagementViewModelSelectionChangedEvent managementViewModelSelectionChangedEvent)
        {
            _managementViewModelSelectionChangedEvent = managementViewModelSelectionChangedEvent;
            EmployeesManagementCommand = new ButtonCommand(OnEmployeesManagementCommandExecute, () => { return true; });
            TeamsManagementCommand = new ButtonCommand(OnTeamsManagementCommandExecute, () => { return true; });
        }

        private void OnTeamsManagementCommandExecute(object obj)
        {
            _managementViewModelSelectionChangedEvent.RaiseManagementViewModelSelectionChangedEvent(nameof(TeamManagementViewModel));
        }

        private void OnEmployeesManagementCommandExecute(object obj)
        {
            _managementViewModelSelectionChangedEvent.RaiseManagementViewModelSelectionChangedEvent(nameof(EmployeeManagementViewModel));
        }


        public Task LoadAsync()
        {
            return null;
        }

    }
}
