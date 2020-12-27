using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using EmployeeManagementTool.Events.Contracts;
using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationViewModel _navigationViewModel;
        private INavigationSelectionChangedEvent _navigationSelectionChangedEvent;

        public INavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set
            {
                _navigationViewModel = value;
                OnPropertyChanged();
            }

        }
        public MainViewModel(INavigationViewModel navigationViewModel, INavigationSelectionChangedEvent navigationSelectionChangedEvent)
        {
            NavigationViewModel = navigationViewModel;
            _navigationSelectionChangedEvent = navigationSelectionChangedEvent;
            _navigationSelectionChangedEvent.OnSelectedNavigationItemChanged+= _navigationSelectionChangedEvent_OnOnSelectedNavigationItemChanged;
        }

        private void _navigationSelectionChangedEvent_OnOnSelectedNavigationItemChanged(object sender, int e)
        {
            MessageBox.Show($"MainViewModel got SelectedNavigationItem Changed event {e}" );
        }

        public async Task OnLoad()
        {

            await NavigationViewModel.LoadAsync();
        }

    }
}
