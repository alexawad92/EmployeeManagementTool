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
    }
}
