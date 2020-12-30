using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
    public class MainViewModel : ViewModelBase
    {
        private readonly IManagementViewModelSelectionChangedEvent _managementViewModelSelectionChangedEvent;
        private readonly IIndex<string, IMainWindowViewModel> _mainViewModelCreator;

        private IMainWindowViewModel _currentViewModel;

        public IMainWindowViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(IManagementViewModelSelectionChangedEvent managementViewModelSelectionChangedEvent, IIndex<string, IMainWindowViewModel> mainViewModelCreator)
        {
            _mainViewModelCreator = mainViewModelCreator;
            _managementViewModelSelectionChangedEvent = managementViewModelSelectionChangedEvent;
            _managementViewModelSelectionChangedEvent.OnManagementViewModelSelectionChanged += _managementViewModelSelectionChangedEvent_OnOnManagementViewModelSelectionChanged;
        }

        public void Load()
        {
            CreateHomeViewModel();
        }

        private async void _managementViewModelSelectionChangedEvent_OnOnManagementViewModelSelectionChanged(object sender, string e)
        {
            switch (e)
            {
                case nameof(EmployeeManagementViewModel):
                    await CreateEmployeeManagementViewModel();
                    break;
                case nameof(TeamManagementViewModel):
                    await CreateTeamManagementViewModel();
                    break;
                case nameof(HomeViewModel):
                    CreateHomeViewModel();
                    break;
                default:
                    throw new Exception("Unknown ViewModel Name");
            }
        }

        private async Task CreateTeamManagementViewModel()
        {
            CurrentViewModel = _mainViewModelCreator[nameof(TeamManagementViewModel)];
            await CurrentViewModel.LoadAsync();
        }

        private void CreateHomeViewModel()
        {
            CurrentViewModel = _mainViewModelCreator[nameof(HomeViewModel)];
        }

        private async Task CreateEmployeeManagementViewModel()
        {
            CurrentViewModel = _mainViewModelCreator[nameof(EmployeeManagementViewModel)];
            await CurrentViewModel.LoadAsync();
        }
    }
}
