using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Autofac.Features.Indexed;

using EmployeeManagementTool.Commands.Impls;
using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.Events.Contracts;
using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public abstract class ManagementViewModelBase: ViewModelBase, IMainWindowViewModel
    {
        private bool _isLoading;
        private string _mainLoadingMessage;
        protected readonly IManagementViewModelSelectionChangedEvent _managementViewModelSelectionChangedEvent;
        protected readonly IDetailViewModelSavedEvent _detailViewModelSavedEvent;
        protected readonly IDetailViewModelDeletedEvent _detailViewModelDeletedEvent;
        protected readonly IIndex<string, IDetailViewModel> _detailViewModelCreator;
        protected INavigationViewModel _navigationViewModel;
        protected INavigationSelectionChangedEvent _navigationSelectionChangedEvent;
        protected IDetailViewModel _detailViewModel;

        public string MainLoadingMessage
        {
            get { return _mainLoadingMessage; }
            set
            {
                _mainLoadingMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                ((ButtonCommand)CreateNewItem).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public ICommand GoHomeCommand { get; set; }
        public ICommand CreateNewItem { get; set; }

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

        protected ManagementViewModelBase(INavigationSelectionChangedEvent navigationSelectionChangedEvent,
                                          IDetailViewModelSavedEvent detailViewModelSavedEvent,
                                          IDetailViewModelDeletedEvent detailViewModelDeletedEvent,
                                          IManagementViewModelSelectionChangedEvent managementViewModelSelectionChangedEvent,
                                          IIndex<string, IDetailViewModel> detailViewModelCreator)
        {
            _managementViewModelSelectionChangedEvent = managementViewModelSelectionChangedEvent;
            _navigationSelectionChangedEvent = navigationSelectionChangedEvent;
            _detailViewModelDeletedEvent = detailViewModelDeletedEvent;
            _detailViewModelDeletedEvent.OnDetailViewModelDeleted += OnDetailViewModelDeleted;
            _navigationSelectionChangedEvent.OnSelectedNavigationItemChanged += OnSelectedNavigationItemChanged;
            _detailViewModelSavedEvent = detailViewModelSavedEvent;
            _detailViewModelCreator = detailViewModelCreator;
            GoHomeCommand = new ButtonCommand(OnGoHomeCommandExecute, () => { return true; });
            CreateNewItem = new ButtonCommand(OnCreateNewItemExecute, CanCreateNewItemBeExecuted);
            MainLoadingMessage = "Please Wait...";
        }

        public bool CanCreateNewItemBeExecuted()
        {
            return !IsLoading;
        }

        private void OnDetailViewModelDeleted(object sender, DetailViewModelDeleteEventArgs e)
        {
            DetailViewModel = null;
        }

        protected abstract void OnSelectedNavigationItemChanged(object sender, int e);

        protected abstract void OnCreateNewItemExecute(object obj);
        
        private void OnGoHomeCommandExecute(object obj)
        {
            _navigationSelectionChangedEvent.OnSelectedNavigationItemChanged -= OnSelectedNavigationItemChanged;
            _detailViewModelDeletedEvent.OnDetailViewModelDeleted -= OnDetailViewModelDeleted;
            _managementViewModelSelectionChangedEvent.RaiseManagementViewModelSelectionChangedEvent(nameof(HomeViewModel));
        }

        public abstract Task LoadAsync();

    }

}
