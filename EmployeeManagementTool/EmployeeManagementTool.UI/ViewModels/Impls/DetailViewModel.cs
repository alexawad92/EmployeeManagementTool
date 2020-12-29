using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using EmployeeManagementTool.Commands.Impls;
using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.DataModel;
using EmployeeManagementTool.ModelWrappers;
using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public class DetailViewModel : ViewModelBase, IDetailViewModel
    {
        private readonly IEmployeeAccessor _employeeAccessor;
        private readonly IEmployeeTypeAccessor _employeeTypeAccessor;
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

        public EmployeeWrapper EmployeeWrapper
        {
            get { return _employeeWrapper; }
            set
            {
                _employeeWrapper = value;
                OnPropertyChanged();
            }
        }
        public DetailViewModel(IEmployeeAccessor employeeAccessor, IEmployeeTypeAccessor employeeTypeAccessor)
        {
            _employeeAccessor = employeeAccessor;
            ViewModelHeader = "Employee Details";
            EmployeeTypes = new ObservableCollection<EmployeeTypeWrapper>();
            _employeeTypeAccessor = employeeTypeAccessor;
            SaveCommand = new ButtonCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private bool OnSaveCanExecute()
        {
            return HasChanges;
        }

        private async void OnSaveExecute(object obj)
        {
            await _employeeAccessor.SaveChangesAsync();
            HasChanges = _employeeAccessor.HasChanges();
        }

        public async Task LoadAsync(int id)
        {
            var employee = await _employeeAccessor.GetEmployeeByIdAsync(id);
            var employeeWrapper = new EmployeeWrapper(employee);
            EmployeeWrapper = employeeWrapper;
            employeeWrapper.PropertyChanged += EmployeeWrapper_OnPropertyChanged;
            await LoadEmployeeTypes();
        }

        private void EmployeeWrapper_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            HasChanges = _employeeAccessor.HasChanges();
  
        }

        private async Task LoadEmployeeTypes()
        {
            var allEmployTypes = await _employeeTypeAccessor.GetAllEmployeeTypesAsync();
            EmployeeTypes.Clear();
            foreach (var employeeType in allEmployTypes)
                EmployeeTypes.Add(new EmployeeTypeWrapper(employeeType));
        }
    }
}
