using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.Events.Contracts;
using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IEmployeeAccessor _employeeAccessor;
        private readonly IEmployeeTypeAccessor _employeeTypeAccessor;
        private readonly IDetailViewModelSavedEvent _detailViewModelSavedEvent;
        private INavigationViewModel _navigationViewModel;
        private INavigationSelectionChangedEvent _navigationSelectionChangedEvent;
        private IDetailViewModel _detailViewModel;

        public IDetailViewModel DetailViewModel
        {
            get { return _detailViewModel; }
            set
            {
                _detailViewModel = value;
                OnPropertyChanged();
            }
        }

        public INavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set
            {
                _navigationViewModel = value;
                OnPropertyChanged();
            }

        }
        public MainViewModel(INavigationViewModel navigationViewModel, INavigationSelectionChangedEvent navigationSelectionChangedEvent, IEmployeeAccessor employeeAccessor, IEmployeeTypeAccessor employeeTypeAccessor, IDetailViewModelSavedEvent detailViewModelSavedEvent)
        {
            _employeeAccessor = employeeAccessor;
            _employeeTypeAccessor = employeeTypeAccessor;
            _detailViewModelSavedEvent = detailViewModelSavedEvent;
            NavigationViewModel = navigationViewModel;
            _navigationSelectionChangedEvent = navigationSelectionChangedEvent;
            _navigationSelectionChangedEvent.OnSelectedNavigationItemChanged+= _navigationSelectionChangedEvent_OnOnSelectedNavigationItemChanged;
        }

        private void _navigationSelectionChangedEvent_OnOnSelectedNavigationItemChanged(object sender, int e)
        {
            DetailViewModel = new DetailViewModel(_employeeAccessor, _employeeTypeAccessor, _detailViewModelSavedEvent);
            DetailViewModel.LoadAsync(e);
        }

        public async Task OnLoad()
        {

            await NavigationViewModel.LoadAsync();
        }

    }
}
