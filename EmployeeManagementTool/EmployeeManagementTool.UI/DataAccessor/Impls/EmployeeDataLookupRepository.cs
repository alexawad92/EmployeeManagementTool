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
    public class EmployeeDataLookupRepository: IEmployeeDataLookupRepository
    {
        private readonly Func<EmployeeManagementToolDbContext> _contextCreator;

        public EmployeeDataLookupRepository(Func<EmployeeManagementToolDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<IEnumerable<LookupItem>> GetLookupItemsAsync()
        {
            List<LookupItem> lookupItems = new List<LookupItem>();
            using (var ctx = _contextCreator())
            {
                var employees=  await Task.Run(() => ctx.Employees.AsNoTracking().ToListAsync());
                foreach (var employee in employees)
                {
                    lookupItems.Add(new LookupItem()
                    {
                        DisplayMember = employee.FirstName + " " + employee.LastName,
                        Id = employee.Id
                    });
                }
            }
            return lookupItems;
        }
    }
}
