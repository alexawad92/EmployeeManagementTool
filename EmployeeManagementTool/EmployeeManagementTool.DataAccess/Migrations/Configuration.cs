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
            context.Teams.AddOrUpdate(m => m.Name,
                new Team()
                {
                    Name = "Thinkers",
                    
                },
                new Team()
                {
                    Name = "Rangers",

                },
                new Team()
                {
                    Name = "Galaxy Defenders",
                    
                },
                new Team()
                {
                    Name = "Einstein",

                });

            context.SaveChanges();
            context.Employees.AddOrUpdate(e => e.FirstName,
                new Employee() {FirstName = "John", LastName = "Kevin", EmployeeType = EmployeeType.SoftwareEngineer, TeamId = context.Teams.Single(t=>t.Name == "Thinkers").Id },
                new Employee() {FirstName = "Alex", LastName = "Awad", EmployeeType = EmployeeType.SoftwareEngineer , TeamId = context.Teams.Single(t => t.Name == "Thinkers").Id },
                new Employee() {FirstName = "Chirstie", LastName = "Jeong", EmployeeType = EmployeeType.SoftwareEngineer, TeamId = context.Teams.Single(t => t.Name == "Thinkers").Id },
                new Employee() {FirstName = "Lisa", LastName = "Fares", EmployeeType = EmployeeType.SoftwareEngineer},
                new Employee() {FirstName = "Ronald", LastName = "Bruce", EmployeeType = EmployeeType.SoftwareEngineer},
                new Employee() {FirstName = "Chandler", LastName = "Salem", EmployeeType = EmployeeType.SoftwareEngineer, TeamId = context.Teams.Single(t => t.Name == "Rangers").Id },
                new Employee() {FirstName = "Caleb", LastName = "Faraj", EmployeeType = EmployeeType.SoftwareEngineer, TeamId = context.Teams.Single(t => t.Name == "Rangers").Id },
                new Employee() {FirstName = "Frank", LastName = "Joey", EmployeeType = EmployeeType.FunctionalManager,  TeamId = context.Teams.Single(t => t.Name == "Rangers").Id },
                new Employee() {FirstName = "Jordan", LastName = "Paul", EmployeeType = EmployeeType.FunctionalManager, TeamId = context.Teams.Single(t => t.Name == "Thinkers").Id },
                new Employee() {FirstName = "James", LastName = "Jordan", EmployeeType = EmployeeType.Tester, TeamId = context.Teams.Single(t => t.Name == "Rangers").Id },
                new Employee() {FirstName = "Monica", LastName = "Lina", EmployeeType = EmployeeType.Tester , TeamId = context.Teams.Single(t => t.Name == "Thinkers").Id });
        }
    }
}
