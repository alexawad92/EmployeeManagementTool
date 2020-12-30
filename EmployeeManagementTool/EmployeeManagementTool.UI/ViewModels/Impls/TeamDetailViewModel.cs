using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using EmployeeManagementTool.Commands.Impls;
using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.DataModel;
using EmployeeManagementTool.Events.Contracts;
using EmployeeManagementTool.ModelWrappers;
using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public class TeamDetailViewModel : ViewModelBase, IDetailViewModel
    {
        private readonly ITeamAccessor _teamAccessor;
        private readonly IDetailViewModelSavedEvent _detailViewModelSavedEvent;
        private Team _team;
        private TeamWrapper _teamWrapper;
        private string _viewModelHeader;
        private bool _hasChanges;
        private IEnumerable<Employee> _allEmployees;
        private Employee _selectedAssignedEmployee;

        public Employee SelectedAssignedEmployee
        {
            get { return _selectedAssignedEmployee; }
            set
            {
                _selectedAssignedEmployee = value;
                OnPropertyChanged();
                ((ButtonCommand)RemoveEmployeeCommand).RaiseCanExecuteChanged();
            }
        }

        private Employee _selectedUnassignedEmployee;

        public Employee SelectedUnassignedEmployee
        {
            get { return _selectedUnassignedEmployee; }
            set
            {
                _selectedUnassignedEmployee = value;
                OnPropertyChanged();
                ((ButtonCommand)AddEmployeeCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Employee> UnassignedEmployees { get; set; }
        public ObservableCollection<Employee> AssignedEmployees { get; set; }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                _hasChanges = value;
                OnPropertyChanged();
                ((ButtonCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public string ViewModelHeader
        {
            get { return _viewModelHeader; }
            set
            {
                _viewModelHeader = value;
                OnPropertyChanged();
            }
        }
        public ICommand SaveCommand { get; set; }
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand RemoveEmployeeCommand { get; set; }

        public TeamWrapper TeamWrapper
        {
            get { return _teamWrapper; }
            set
            {
                _teamWrapper = value;
                OnPropertyChanged();
            }
        }

        public TeamDetailViewModel(ITeamAccessor teamAccessor , IDetailViewModelSavedEvent detailViewModelSavedEvent)
        {
            _teamAccessor = teamAccessor;
            _detailViewModelSavedEvent = detailViewModelSavedEvent;
            ViewModelHeader = "Team Details";
            SaveCommand = new ButtonCommand(OnSaveExecute, OnSaveCanExecute);
            AddEmployeeCommand = new ButtonCommand(OnAddEmployeeCommandExecuted, CanAddEmployeeCommandBeExecuted);
            RemoveEmployeeCommand = new ButtonCommand(OnRemoveEmployeeCommandExecuted, CanRemoveEmployeeCommandBeExecuted);
            AssignedEmployees = new ObservableCollection<Employee>();
            UnassignedEmployees = new ObservableCollection<Employee>();
        }

        private bool CanRemoveEmployeeCommandBeExecuted()
        {
            return SelectedAssignedEmployee != null;

        }

        private void OnRemoveEmployeeCommandExecuted(object obj)
        {
            _team.Employees.Remove(SelectedAssignedEmployee);
            UnassignedEmployees.Add(SelectedAssignedEmployee);
            AssignedEmployees.Remove(SelectedAssignedEmployee);
            HasChanges = _teamAccessor.HasChanges();
        }

        private bool CanAddEmployeeCommandBeExecuted()
        {
            return SelectedUnassignedEmployee != null;
        }

        private void OnAddEmployeeCommandExecuted(object obj)
        {
            _team.Employees.Add(SelectedUnassignedEmployee);
            AssignedEmployees.Add(SelectedUnassignedEmployee);
            UnassignedEmployees.Remove(SelectedUnassignedEmployee);
            HasChanges = _teamAccessor.HasChanges();
        }

        private bool OnSaveCanExecute()
        {
            return HasChanges;
        }

        private async void OnSaveExecute(object obj)
        {
            await _teamAccessor.SaveChangesAsync();
            HasChanges = _teamAccessor.HasChanges();

            _detailViewModelSavedEvent.RaiseDetailViewModelSavedEvent(new DetailViewModelSavedEventArgs()
            {
                Id = TeamWrapper.Id,
                DisplayMember = $"{TeamWrapper.Name}"
            });
        }


        public async Task LoadAsync(int id)
        {
            _team = id > 0 ? await _teamAccessor.GetEntityByIdAsync(id) : CreateNewTeam();
            _allEmployees = await _teamAccessor.GetAllEmployeesAsync();
            foreach (var assignedEmployee in _team.Employees)
            {
                AssignedEmployees.Add(assignedEmployee);
            }

            foreach (var unassignedEmployee in _allEmployees.Where(e=>e.TeamId == null))
            {
                UnassignedEmployees.Add(unassignedEmployee);
            }

            var teamWrapper = new TeamWrapper(_team);
            TeamWrapper = teamWrapper;
            teamWrapper.PropertyChanged += TeamWrapper_OnPropertyChanged;
        }

        private Team CreateNewTeam()
        {
            Team team = new Team();
            _teamAccessor.Add(team);
            return team;
        }

        private void TeamWrapper_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            HasChanges = _teamAccessor.HasChanges();
        }

        
    }
}
