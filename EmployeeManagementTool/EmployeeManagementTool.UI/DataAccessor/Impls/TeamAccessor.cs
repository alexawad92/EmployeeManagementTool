using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataAccess;
using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.DataModel;


namespace EmployeeManagementTool.DataAccessor.Impls
{
    public class TeamAccessor: DatabaseRepository<Team>, ITeamAccessor
    {
        public TeamAccessor(EmployeeManagementToolDbContext context) : base(context)
        {
        }

        public override  async Task<IEnumerable<Team>> GetAllEntitiesAsync()
        {
            return await Task.Run(() => Context.Teams.Include(t=>t.Employees).ToListAsync());
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await Task.Run(() => Context.Employees.ToListAsync());
        }

    }
}
