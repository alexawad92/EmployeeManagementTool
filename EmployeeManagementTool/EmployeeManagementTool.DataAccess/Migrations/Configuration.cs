﻿using System.Collections.Generic;

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

            context.EmployeeTypes.AddOrUpdate(ey=>ey.JobTitle, new EmployeeType(){ JobTitle = "Tester"},
                new EmployeeType() { JobTitle = "FunctionalManager" },
                new EmployeeType() { JobTitle = "SoftwareEngineer"});
            context.SaveChanges();
            context.Employees.AddOrUpdate(e => e.FirstName,
                new Employee() {FirstName = "John", LastName = "Kevin", EmployeeTypeId = context.EmployeeTypes.Single(type=>type.JobTitle == "SoftwareEngineer").Id, TeamId = context.Teams.Single(t=>t.Name == "Thinkers").Id, DateOfBirth = new DateTime(1978, 1, 1), Gender = Gender.Male},
                new Employee() {FirstName = "Alex", LastName = "Awad", EmployeeTypeId = context.EmployeeTypes.Single(type => type.JobTitle == "SoftwareEngineer").Id, TeamId = context.Teams.Single(t => t.Name == "Thinkers").Id, DateOfBirth = new DateTime(1972, 4, 22), Gender = Gender.Male },
                new Employee() {FirstName = "Chirstie", LastName = "Jeong", EmployeeTypeId = context.EmployeeTypes.Single(type => type.JobTitle == "SoftwareEngineer").Id, TeamId = context.Teams.Single(t => t.Name == "Thinkers").Id, DateOfBirth = new DateTime(1963, 12, 1) , Gender = Gender.Female },
                new Employee() {FirstName = "Lisa", LastName = "Fares", EmployeeTypeId = context.EmployeeTypes.Single(type => type.JobTitle == "SoftwareEngineer").Id, DateOfBirth = new DateTime(1988,6 , 20), TeamId = context.Teams.Single(t => t.Name == "Thinkers").Id, Gender = Gender.Female },
                new Employee() {FirstName = "Ronald", LastName = "Bruce", EmployeeTypeId = context.EmployeeTypes.Single(type => type.JobTitle == "SoftwareEngineer").Id, DateOfBirth = new DateTime(1996, 3, 16), TeamId = context.Teams.Single(t => t.Name == "Thinkers").Id, Gender = Gender.Male },
                new Employee() {FirstName = "Chandler", LastName = "Salem", EmployeeTypeId = context.EmployeeTypes.Single(type => type.JobTitle == "SoftwareEngineer").Id, TeamId = context.Teams.Single(t => t.Name == "Rangers").Id, DateOfBirth = new DateTime(1990, 3, 13), Gender = Gender.Male },
                new Employee() {FirstName = "Caleb", LastName = "Faraj", EmployeeTypeId = context.EmployeeTypes.Single(type => type.JobTitle == "SoftwareEngineer").Id, TeamId = context.Teams.Single(t => t.Name == "Rangers").Id, DateOfBirth = new DateTime(1968, 8, 22), Gender = Gender.Male },
                new Employee() {FirstName = "Frank", LastName = "Joey", EmployeeTypeId = context.EmployeeTypes.Single(type => type.JobTitle == "FunctionalManager").Id, TeamId = context.Teams.Single(t => t.Name == "Rangers").Id, DateOfBirth = new DateTime(1970, 9, 1), Gender = Gender.Male },
                new Employee() {FirstName = "Jordan", LastName = "Paul", EmployeeTypeId = context.EmployeeTypes.Single(type => type.JobTitle == "FunctionalManager").Id, TeamId = context.Teams.Single(t => t.Name == "Thinkers").Id, DateOfBirth = new DateTime(1985, 4, 1), Gender = Gender.Other },
                new Employee() {FirstName = "James", LastName = "Jordan", EmployeeTypeId = context.EmployeeTypes.Single(type => type.JobTitle == "Tester").Id, TeamId = context.Teams.Single(t => t.Name == "Rangers").Id, DateOfBirth = new DateTime(1986, 3, 1), Gender = Gender.Male },
                new Employee() {FirstName = "Monica", LastName = "Lina", EmployeeTypeId = context.EmployeeTypes.Single(type => type.JobTitle == "Tester").Id, TeamId = context.Teams.Single(t => t.Name == "Thinkers").Id, DateOfBirth = new DateTime(1992, 2, 2), Gender = Gender.Female });
        }
    }
}
