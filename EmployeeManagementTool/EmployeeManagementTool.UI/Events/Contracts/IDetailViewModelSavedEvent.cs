using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementTool.Events.Contracts
{
    public interface IDetailViewModelSavedEvent
    {
        void RaiseDetailViewModelSavedEvent(int id);
        event EventHandler<int> OnDetailViewModelSaved;
    }
}
