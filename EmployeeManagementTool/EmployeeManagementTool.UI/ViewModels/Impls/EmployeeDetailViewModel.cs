using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    public class EmployeeDetailViewModel : ViewModelBase, IDetailViewModel
    {
        private readonly IEmployeeAccessor _employeeAccessor;
        private Employee _employee;
        private readonly IEmployeeTypeAccessor _employeeTypeAccessor;
        private readonly IDetailViewModelSavedEvent _detailViewModelSavedEvent;
        private readonly IDetailViewModelDeletedEvent _detailViewModelDeletedEvent;
        private readonly IMessageDialogService _messageDialogService;
        private EmployeeWrapper _employeeWrapper;
        private string _viewModelHeader;
        private bool _hasChanges;

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

        public ObservableCollection<EmployeeTypeWrapper> EmployeeTypes { get; set; }

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

        public EmployeeWrapper EmployeeWrapper
        {
            get { return _employeeWrapper; }
            set
            {
                _employeeWrapper = value;
                OnPropertyChanged();
            }
        }
        public EmployeeDetailViewModel(IEmployeeAccessor employeeAccessor, IEmployeeTypeAccessor employeeTypeAccessor, IDetailViewModelSavedEvent detailViewModelSavedEvent, 
                                       IDetailViewModelDeletedEvent detailViewModelDeletedEvent, IMessageDialogService messageDialogService)
        {
            _employeeAccessor = employeeAccessor;
            _detailViewModelSavedEvent = detailViewModelSavedEvent;
            _detailViewModelDeletedEvent = detailViewModelDeletedEvent;
            _messageDialogService = messageDialogService;
            ViewModelHeader = "Employee Details";
            EmployeeTypes = new ObservableCollection<EmployeeTypeWrapper>();
            _employeeTypeAccessor = employeeTypeAccessor;
            SaveCommand = new ButtonCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new ButtonCommand(OnDeleteExecute, () => { return true;});
        }

        private async void OnDeleteExecute(object obj)
        {
            int idToBeDeleted = _employee.Id;
            string displayMemberToBeDeleted = _employee.FirstName + " " + _employee.LastName;
            var result = await _employeeAccessor.IsPartOfAnyTeam(idToBeDeleted);
            if (result != null)
            {
                await _messageDialogService.ShowInfoDialogAsync($"{displayMemberToBeDeleted} cannot be deleted because it is part of {result.Name} team", "Warning");
                return;
            }

            var userChoice = await
                _messageDialogService.ShowOkCancelDialogAsync(
                    $"{displayMemberToBeDeleted} is about to be deleted from the database. Are you sure you want to continue?", "Confirm Deletion");
            if(userChoice == MessageDialogResult.Cancel)
                return;
            _employeeAccessor.Remove(_employee);
            await _employeeAccessor.SaveChangesAsync();
            _detailViewModelDeletedEvent.RaiseDetailViewModelDeletedEvent(new DetailViewModelDeleteEventArgs()
            {
                Id = idToBeDeleted, 
                DisplayMember = displayMemberToBeDeleted
            });

        }

        private bool OnSaveCanExecute()
        {
            return HasChanges;
        }

        private async void OnSaveExecute(object obj)
        {
            await _employeeAccessor.SaveChangesAsync();
            HasChanges = _employeeAccessor.HasChanges();

            _detailViewModelSavedEvent.RaiseDetailViewModelSavedEvent(new DetailViewModelSavedEventArgs()
            {
                Id = EmployeeWrapper.Id,
                DisplayMember = $"{EmployeeWrapper.FirstName} {EmployeeWrapper.LastName}"
            });
            
        }

        public async Task LoadAsync(int id)
        {
            _employee = id > 0 ? await _employeeAccessor.GetEntityByIdAsync(id) : CreateNewEmployee();
            var employeeWrapper = new EmployeeWrapper(_employee);
            EmployeeWrapper = employeeWrapper;
            employeeWrapper.PropertyChanged += EmployeeWrapper_OnPropertyChanged;
            await LoadEmployeeTypes();
        }

        private Employee CreateNewEmployee()
        {
            Employee employee = new Employee();
            _employeeAccessor.Add(employee);
            return employee;
        }

        private void EmployeeWrapper_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            HasChanges = _employeeAccessor.HasChanges();
        }

        private async Task LoadEmployeeTypes()
        {
            var allEmployTypes = await _employeeTypeAccessor.GetAllEntitiesAsync();
            EmployeeTypes.Clear();
            foreach (var employeeType in allEmployTypes)
                EmployeeTypes.Add(new EmployeeTypeWrapper(employeeType));
        }
    }
}
