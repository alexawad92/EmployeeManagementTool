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
    public class EmployeeTypeAccessor : DatabaseRepository<EmployeeType>, IEmployeeTypeAccessor
    {
        public EmployeeTypeAccessor(EmployeeManagementToolDbContext context) : base(context)
        {
        }
    }
}
