using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.Include(e=>e.EmployeeType).SingleAsync(employee => employee.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task ReloadEmployeeAsync(int id)
        {
            var dbEntityEntry = _context.ChangeTracker.Entries<Employee>().SingleOrDefault(db => db.Entity.Id == id);
            if (dbEntityEntry != null)
            {
                await dbEntityEntry.ReloadAsync();
            }
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
