using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataModel;


namespace EmployeeManagementTool.DataAccessor.Contracts
{
    public interface IEmployeeAccessor : IDatabaseRepository<Employee>
    {
        Task<Team> IsPartOfAnyTeam(int employeeId);
    }
}
