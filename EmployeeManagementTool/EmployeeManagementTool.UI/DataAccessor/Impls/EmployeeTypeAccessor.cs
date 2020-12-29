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
    public class EmployeeTypeAccessor : IEmployeeTypeAccessor
    {
        private readonly EmployeeManagementToolDbContext _context;
        public EmployeeTypeAccessor(EmployeeManagementToolDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EmployeeType>> GetAllEmployeeTypesAsync()
        {
            return await _context.EmployeeTypes.ToListAsync();
        }
    }
}
