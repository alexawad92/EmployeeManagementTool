using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using Autofac.Features.Indexed;

using EmployeeManagementTool.Commands.Impls;
using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.Events.Contracts;
using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public class EmployeeManagementViewModel : ManagementViewModelBase
    {
        private readonly IEmployeeDataLookupRepository _employeeDataLookupRepository;
        private string _subMessage;

        public string SubMessage
        {
            get { return _subMessage; }
            set
            {
                _subMessage = value;
                OnPropertyChanged();
            }
        }
        public EmployeeManagementViewModel(INavigationSelectionChangedEvent navigationSelectionChangedEvent, 
                                           IDetailViewModelSavedEvent detailViewModelSavedEvent,
                                           IDetailViewModelDeletedEvent detailViewModelDeletedEvent,
                                           IManagementViewModelSelectionChangedEvent managementViewModelSelectionChangedEvent, 
                                           IEmployeeDataLookupRepository employeeDataLookupRepository,
                                           IIndex<string, IDetailViewModel> detailViewModelCreator): 
            base(navigationSelectionChangedEvent, detailViewModelSavedEvent, detailViewModelDeletedEvent, managementViewModelSelectionChangedEvent, detailViewModelCreator)
        {
            _employeeDataLookupRepository = employeeDataLookupRepository;
        }

        protected override void OnCreateNewItemExecute(object obj)
        {
            DetailViewModel = _detailViewModelCreator[nameof(EmployeeDetailViewModel)];
            DetailViewModel.LoadAsync(-1);
        }

        protected override void OnSelectedNavigationItemChanged(object sender, int e)
        {
            DetailViewModel = _detailViewModelCreator[nameof(EmployeeDetailViewModel)];
            DetailViewModel.LoadAsync(e);
        }

        public override async Task LoadAsync()
        {
            SubMessage = "Employees are being loaded from the database";
            IsLoading = true;
            NavigationViewModel = new NavigationViewModel(_employeeDataLookupRepository, _navigationSelectionChangedEvent, _detailViewModelSavedEvent, _detailViewModelDeletedEvent);
            await NavigationViewModel.LoadAsync();
            IsLoading = false;
        }
    }
}
