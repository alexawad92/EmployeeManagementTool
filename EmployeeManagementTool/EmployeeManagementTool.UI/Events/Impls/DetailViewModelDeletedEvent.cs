using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.Events.Contracts;


namespace EmployeeManagementTool.Events.Impls
{
    public class DetailViewModelDeletedEvent : IDetailViewModelDeletedEvent
    {
        public void RaiseDetailViewModelDeletedEvent(DetailViewModelDeleteEventArgs arg)
        {
            OnDetailViewModelDeleted?.Invoke(this, arg);
        }

        public event EventHandler<DetailViewModelDeleteEventArgs> OnDetailViewModelDeleted;
    }
}
