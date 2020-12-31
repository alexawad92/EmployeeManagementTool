using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using EmployeeManagementTool.DataAccess;
using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.DataModel;

namespace EmployeeManagementTool.DataAccessor.Impls
{
    public class EmployeeAccessor : DatabaseRepository<Employee> , IEmployeeAccessor
    {
        public EmployeeAccessor(EmployeeManagementToolDbContext context) : base(context)
        {
        }

        public async Task<Team> IsPartOfAnyTeam(int employeeId)
        {
            return await Context.Teams.AsNoTracking().Include(t => t.Employees)
                       .SingleOrDefaultAsync(t => t.Employees.Any(e => e.Id == employeeId));
        }
    }
}
