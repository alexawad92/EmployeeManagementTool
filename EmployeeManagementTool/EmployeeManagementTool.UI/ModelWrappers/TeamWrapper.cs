using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataModel;
using EmployeeManagementTool.ViewModels.Impls;


namespace EmployeeManagementTool.ModelWrappers
{
    public class TeamWrapper : ViewModelBase
    {
        private readonly Team _team;

        public TeamWrapper(Team team)
        {
            _team = team;
        }
        public int Id => _team.Id;

        public string Name
        {
            get { return _team.Name; }
            set
            {
                _team.Name= value;
                OnPropertyChanged();
            }
        }
    }
}
