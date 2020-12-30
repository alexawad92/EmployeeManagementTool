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
    public class TeamManagementViewModel: ManagementViewModelBase
    {
        private readonly ITeamDataLookupRepository _teamDataLookupRepository;

        public TeamManagementViewModel(INavigationSelectionChangedEvent navigationSelectionChangedEvent,
                                       IDetailViewModelSavedEvent detailViewModelSavedEvent,
                                       IManagementViewModelSelectionChangedEvent managementViewModelSelectionChangedEvent,
                                       ITeamDataLookupRepository teamDataLookupRepository,
                                       IIndex<string, IDetailViewModel> detailViewModelCreator) :
            base(navigationSelectionChangedEvent, detailViewModelSavedEvent, managementViewModelSelectionChangedEvent, detailViewModelCreator)
        {
            _teamDataLookupRepository = teamDataLookupRepository;
        }

        protected override void OnCreateNewItemExecute(object obj)
        {
            DetailViewModel = _detailViewModelCreator[nameof(TeamDetailViewModel)];
            DetailViewModel.LoadAsync(-1);
        }

        protected override void OnSelectedNavigationItemChanged(object sender, int e)
        {
            DetailViewModel = _detailViewModelCreator[nameof(TeamDetailViewModel)];
            DetailViewModel.LoadAsync(e);
        }

        public override async Task LoadAsync()
        {
            NavigationViewModel = new NavigationViewModel(_teamDataLookupRepository, _navigationSelectionChangedEvent, _detailViewModelSavedEvent);
            await NavigationViewModel.LoadAsync();
        }
    }
}
