using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.Events.Contracts;


namespace EmployeeManagementTool.Events.Impls
{
    public class DetailViewModelSavedEvent : IDetailViewModelSavedEvent
    {
        public void RaiseDetailViewModelSavedEvent(int id)
        {
            OnDetailViewModelSaved?.Invoke(this, id);
        }

        public event EventHandler<int> OnDetailViewModelSaved;
    }
}
