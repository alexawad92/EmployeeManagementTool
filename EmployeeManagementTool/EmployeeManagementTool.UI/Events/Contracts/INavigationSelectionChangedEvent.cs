using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementTool.Events.Contracts
{
    public interface INavigationSelectionChangedEvent
    {
        void RaiseSelectedNavigationItemChangedEvent(int navigationItemId);
        event EventHandler<int> OnSelectedNavigationItemChanged;
    }
}
