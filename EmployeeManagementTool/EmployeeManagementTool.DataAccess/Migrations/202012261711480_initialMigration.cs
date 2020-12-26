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
                        EmployeeType = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.TeamEmployee",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Employee_Id })
                .ForeignKey("dbo.Team", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamEmployee", "Employee_Id", "dbo.Employee");
            DropForeignKey("dbo.TeamEmployee", "Team_Id", "dbo.Team");
            DropIndex("dbo.TeamEmployee", new[] { "Employee_Id" });
            DropIndex("dbo.TeamEmployee", new[] { "Team_Id" });
            DropTable("dbo.TeamEmployee");
            DropTable("dbo.Team");
            DropTable("dbo.Employee");
        }
    }
}
