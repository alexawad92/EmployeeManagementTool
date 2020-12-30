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
        public void RaiseDetailViewModelSavedEvent(DetailViewModelSavedEventArgs arg)
        {
            OnDetailViewModelSaved?.Invoke(this, arg);
        }

        public event EventHandler<DetailViewModelSavedEventArgs> OnDetailViewModelSaved;
    }

}
