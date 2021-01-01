using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataModel;
using EmployeeManagementTool.ViewModels.Impls;


namespace EmployeeManagementTool.ModelWrappers
{
    public class TeamWrapper : WrapperBase<Team>
    {

        public TeamWrapper(Team team) :base(team)
        {
        }
        public int Id => Model.Id;

        public string Name
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }
    }
}
