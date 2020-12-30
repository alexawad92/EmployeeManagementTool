namespace EmployeeManagementTool.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        EmployeeTypeId = c.Int(nullable: false),
                        TeamId = c.Int(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeType", t => t.EmployeeTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.TeamId)
                .Index(t => t.EmployeeTypeId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.EmployeeType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "TeamId", "dbo.Team");
            DropForeignKey("dbo.Employee", "EmployeeTypeId", "dbo.EmployeeType");
            DropIndex("dbo.Employee", new[] { "TeamId" });
            DropIndex("dbo.Employee", new[] { "EmployeeTypeId" });
            DropTable("dbo.Team");
            DropTable("dbo.EmployeeType");
            DropTable("dbo.Employee");
        }
    }
}
