using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.Events.Contracts;


namespace EmployeeManagementTool.Events.Impls
{
    public class NavigationSelectionChangedEvent : INavigationSelectionChangedEvent
    {
        public void RaiseSelectedNavigationItemChangedEvent(int navigationItemId)
        {
            OnSelectedNavigationItemChanged?.Invoke(this, navigationItemId);
        }

        public event EventHandler<int> OnSelectedNavigationItemChanged;
    }
}
