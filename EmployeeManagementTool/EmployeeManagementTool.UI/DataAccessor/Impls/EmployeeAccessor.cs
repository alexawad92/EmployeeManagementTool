using System;
using System.CodeDom;
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
    public class EmployeeAccessor : IEmployeeAccessor
    {
        private readonly EmployeeManagementToolDbContext _context;
        public EmployeeAccessor(EmployeeManagementToolDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }
    }
}
