﻿using System;
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
using EmployeeManagementTool.Views.Services.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public class TeamDetailViewModel : ViewModelBase, IDetailViewModel
    {
        private readonly ITeamAccessor _teamAccessor;
        private readonly IDetailViewModelSavedEvent _detailViewModelSavedEvent;
        private readonly IDetailViewModelDeletedEvent _detailViewModelDeletedEvent;
        private readonly IMessageDialogService _messageDialogService;
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
        public ICommand DeleteCommand { get; set; }
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

        public TeamDetailViewModel(ITeamAccessor teamAccessor , IDetailViewModelSavedEvent detailViewModelSavedEvent,
                                   IDetailViewModelDeletedEvent detailViewModelDeletedEvent, IMessageDialogService messageDialogService)
        {
            _teamAccessor = teamAccessor;
            _detailViewModelSavedEvent = detailViewModelSavedEvent;
            _detailViewModelDeletedEvent = detailViewModelDeletedEvent;
            _messageDialogService = messageDialogService;
            ViewModelHeader = "Team Details";
            SaveCommand = new ButtonCommand(OnSaveExecute, OnSaveCanExecute);
            AddEmployeeCommand = new ButtonCommand(OnAddEmployeeCommandExecuted, CanAddEmployeeCommandBeExecuted);
            RemoveEmployeeCommand = new ButtonCommand(OnRemoveEmployeeCommandExecuted, CanRemoveEmployeeCommandBeExecuted);
            AssignedEmployees = new ObservableCollection<Employee>();
            UnassignedEmployees = new ObservableCollection<Employee>();
            DeleteCommand = new ButtonCommand(OnDeleteExecute, () => { return true; });
        }

        private async void OnDeleteExecute(object obj)
        {
            int idToBeDeleted = _team.Id;
            string displayMemberToBeDeleted = _team.Name;

            var userChoice = await
                                 _messageDialogService.ShowOkCancelDialogAsync(
                                     $"{displayMemberToBeDeleted} is about to be deleted from the database. Are you sure you want to continue?", "Confirm Deletion");
            if (userChoice == MessageDialogResult.Cancel)
                return;

            TeamWrapper.PropertyChanged -= TeamWrapper_OnPropertyChanged;
            TeamWrapper.ErrorsChanged -= TeamWrapper_OnErrorChanged;
            _teamAccessor.Remove(_team);
            await _teamAccessor.SaveChangesAsync();
            _detailViewModelDeletedEvent.RaiseDetailViewModelDeletedEvent(new DetailViewModelDeleteEventArgs()
            {
                Id = idToBeDeleted,
                DisplayMember = displayMemberToBeDeleted
            });
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
            return TeamWrapper!=null && HasChanges && !TeamWrapper.HasErrors;
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
            if (TeamWrapper != null)
            {
                TeamWrapper.PropertyChanged -= TeamWrapper_OnPropertyChanged;
                TeamWrapper.ErrorsChanged -= TeamWrapper_OnErrorChanged;
            }
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
            if (TeamWrapper.Id == 0)
                TeamWrapper.Name = string.Empty;
            teamWrapper.PropertyChanged += TeamWrapper_OnPropertyChanged;
            teamWrapper.ErrorsChanged += TeamWrapper_OnErrorChanged;
        }

        private void TeamWrapper_OnErrorChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ((ButtonCommand)SaveCommand).RaiseCanExecuteChanged();
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
