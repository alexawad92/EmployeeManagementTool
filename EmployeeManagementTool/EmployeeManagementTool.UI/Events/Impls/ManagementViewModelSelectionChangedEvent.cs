using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.Events.Contracts;


namespace EmployeeManagementTool.Events.Impls
{
    public class ManagementViewModelSelectionChangedEvent: IManagementViewModelSelectionChangedEvent
    {
        public void RaiseManagementViewModelSelectionChangedEvent(string managementViewModelName)
        {
            OnManagementViewModelSelectionChanged?.Invoke(this, managementViewModelName);
        }

        public event EventHandler<string> OnManagementViewModelSelectionChanged;
    }
}
