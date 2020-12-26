using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataModel;


namespace EmployeeManagementTool.DataAccess
{
    public class EmployeeManagementToolDbContext : DbContext
    {
        public EmployeeManagementToolDbContext() : base("EmployeeManagementToolDb")
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
