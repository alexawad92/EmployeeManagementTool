using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationViewModel _navigationViewModel;

        public INavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
            set
            {
                _navigationViewModel = value;
                OnPropertyChanged();
            }

        }
        public MainViewModel(INavigationViewModel navigationViewModel)
        {
            NavigationViewModel = navigationViewModel;
        }

        public async Task OnLoad()
        {

            await NavigationViewModel.LoadAsync();
        }

    }
}
