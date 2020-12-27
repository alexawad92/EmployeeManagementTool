using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

using EmployeeManagementTool.Commands.Impls;
using EmployeeManagementTool.DataModel;
using EmployeeManagementTool.Events.Contracts;
using EmployeeManagementTool.ViewModels.Contracts;


namespace EmployeeManagementTool.ViewModels.Impls
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private string _displayMember;
        private readonly INavigationSelectionChangedEvent _navigationSelectionChangedEvent;

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value; 
                OnPropertyChanged();
            }
        }

        public int Id { get; }
        public ICommand NavigationItemClickCommand { get; set; }

        public NavigationItemViewModel(int id, string displayMember, INavigationSelectionChangedEvent navigationSelectionChangedEvent)
        {
            Id = id;
            DisplayMember = displayMember;
            _navigationSelectionChangedEvent = navigationSelectionChangedEvent;
            NavigationItemClickCommand = new ButtonCommand(OnExecute, CanExecute);
        }

        private bool CanExecute()
        {
            return true;
        }

        private void OnExecute(object obj)
        {
            _navigationSelectionChangedEvent.RaiseSelectedNavigationItemChangedEvent(this.Id);
        }
    }
}
