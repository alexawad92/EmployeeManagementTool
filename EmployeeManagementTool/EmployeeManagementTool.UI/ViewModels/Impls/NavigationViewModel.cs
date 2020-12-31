using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using EmployeeManagementTool.Commands.Impls;
using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.DataModel;
using EmployeeManagementTool.Events.Contracts;
using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    /// <summary>
    ///     Represents the navigation panel on the left side
    /// </summary>
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IDataLookupRepository _dataLookupRepository;
        private readonly INavigationSelectionChangedEvent _navigationSelectionChangedEvent;
        private readonly IDetailViewModelDeletedEvent _detailViewModelDeletedEvent;

        private readonly IDetailViewModelSavedEvent _detailViewModelSavedEvent;
        public ObservableCollection<NavigationItemViewModel> NavigationItemViewModels { get; set; }
    
        public NavigationViewModel(IDataLookupRepository dataLookupRepository, 
                                   INavigationSelectionChangedEvent navigationSelectionChangedEvent, 
                                   IDetailViewModelSavedEvent detailViewModelSavedEvent,
                                   IDetailViewModelDeletedEvent detailViewModelDeletedEvent)
        {
            _dataLookupRepository = dataLookupRepository;
            _navigationSelectionChangedEvent = navigationSelectionChangedEvent;
            _detailViewModelSavedEvent = detailViewModelSavedEvent;
            _detailViewModelSavedEvent.OnDetailViewModelSaved += _detailViewModelSavedEvent_OnOnDetailViewModelSaved;
            _detailViewModelDeletedEvent = detailViewModelDeletedEvent;
            _detailViewModelDeletedEvent.OnDetailViewModelDeleted += OnDetailViewModelDeleted;
            NavigationItemViewModels = new ObservableCollection<NavigationItemViewModel>();
        }

        private void OnDetailViewModelDeleted(object sender, DetailViewModelDeleteEventArgs args)
        {
            NavigationItemViewModel navigationItemViewModelToBeRemoved = NavigationItemViewModels.SingleOrDefault(e => e.Id == args.Id
                                                                                                                       && e.DisplayMember ==
                                                                                                                       args.DisplayMember);
            if (navigationItemViewModelToBeRemoved != null)
                NavigationItemViewModels.Remove(navigationItemViewModelToBeRemoved);
        }

        private void _detailViewModelSavedEvent_OnOnDetailViewModelSaved(object sender, DetailViewModelSavedEventArgs arg)
        {
            var outdatedNavigationItemViewModel = NavigationItemViewModels.SingleOrDefault(navigationItem => navigationItem.Id == arg.Id);
            if (outdatedNavigationItemViewModel == null)
            {
                outdatedNavigationItemViewModel = new NavigationItemViewModel(arg.Id, arg.DisplayMember, _navigationSelectionChangedEvent);
                NavigationItemViewModels.Add(outdatedNavigationItemViewModel);
            }
            else
            {
                outdatedNavigationItemViewModel.DisplayMember = arg.DisplayMember;
            }
        }

        public async Task LoadAsync()
        {
            var lookupItems = await _dataLookupRepository.GetLookupItemsAsync();
            foreach (var lookupItem in lookupItems)
            {
                string displayMember = lookupItem.DisplayMember;
                NavigationItemViewModels.Add(new NavigationItemViewModel(lookupItem.Id, displayMember, _navigationSelectionChangedEvent));
            }
        }
    }
}
