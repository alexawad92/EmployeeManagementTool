using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementTool.Events.Contracts
{
    public interface IDetailViewModelDeletedEvent
    {
        void RaiseDetailViewModelDeletedEvent(DetailViewModelDeleteEventArgs arg);
        event EventHandler<DetailViewModelDeleteEventArgs> OnDetailViewModelDeleted;
    }
    public class DetailViewModelDeleteEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}