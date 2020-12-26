using System.Collections.Generic;

using EmployeeManagementTool.DataModel;


namespace EmployeeManagementTool.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeManagementTool.DataAccess.EmployeeManagementToolDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EmployeeManagementTool.DataAccess.EmployeeManagementToolDbContext context)
        {
           context.Employees.AddOrUpdate(f=>f.FirstName, 
               new Employee(){FirstName = "John", LastName = "Kevin", EmployeeType = EmployeeType.SoftwareEngineer},
               new Employee() { FirstName = "Alex", LastName = "Awad", EmployeeType = EmployeeType.SoftwareEngineer },
               new Employee() { FirstName = "Chirstie", LastName = "Jeong", EmployeeType = EmployeeType.SoftwareEngineer },
               new Employee() { FirstName = "Lisa", LastName = "Fares", EmployeeType = EmployeeType.SoftwareEngineer },
               new Employee() { FirstName = "Ronald", LastName = "Bruce", EmployeeType = EmployeeType.SoftwareEngineer },
               new Employee() { FirstName = "Chandler", LastName = "Salem", EmployeeType = EmployeeType.SoftwareEngineer },
               new Employee() { FirstName = "Caleb", LastName = "Faraj", EmployeeType = EmployeeType.SoftwareEngineer },
               new Employee() { FirstName = "Frank", LastName = "Joey", EmployeeType = EmployeeType.FunctionalManager},
               new Employee() { FirstName = "Jordan", LastName = "Paul", EmployeeType = EmployeeType.FunctionalManager },
               new Employee() { FirstName = "James", LastName = "Jordan", EmployeeType = EmployeeType.Tester},
               new Employee() { FirstName = "Monica", LastName = "Lina", EmployeeType = EmployeeType.Tester });

            //context.SaveChanges();

            context.Teams.AddOrUpdate(m => m.Name, new Team()
            {
                Name = "Thinkers",
                Employees = new List<Employee>()
               {
                   context.Employees.Single(e=>e.FirstName == "John" && e.LastName == "Kevin"),
                   context.Employees.Single(e=>e.FirstName == "Alex" && e.LastName == "Awad"),
                   context.Employees.Single(e=>e.FirstName == "Lisa" && e.LastName == "Fares"),
                   context.Employees.Single(e=>e.FirstName == "Frank" && e.LastName == "Joey"),

               }

            },
                new Team()
                {
                    Name = "Rangers",
                    Employees = new List<Employee>()
                    {
                        context.Employees.Single(e=>e.FirstName == "Alex" && e.LastName == "Awad"),
                        context.Employees.Single(e=>e.FirstName == "James" && e.LastName == "Jordan"),
                        context.Employees.Single(e=>e.FirstName == "Monica" && e.LastName == "Lina"),
                        context.Employees.Single(e=>e.FirstName == "Caleb" && e.LastName == "Faraj"),

                    }

                });
        }
    }
}
