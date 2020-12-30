using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementTool.Events.Contracts
{
    public interface IManagementViewModelSelectionChangedEvent
    {
        void RaiseManagementViewModelSelectionChangedEvent(string managementViewModelName);
        event EventHandler<string> OnManagementViewModelSelectionChanged;
    }
}
