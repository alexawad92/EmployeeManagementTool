using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementTool.Events.Contracts
{
    public interface IDetailViewModelSavedEvent
    {
        void RaiseDetailViewModelSavedEvent(DetailViewModelSavedEventArgs arg);
        event EventHandler<DetailViewModelSavedEventArgs> OnDetailViewModelSaved;
    }
    public class DetailViewModelSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}
