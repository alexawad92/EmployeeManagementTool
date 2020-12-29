namespace EmployeeManagementTool.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTeamIdToBeMandatoryField : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employee", "TeamId", "dbo.Team");
            DropIndex("dbo.Employee", new[] { "TeamId" });
            AlterColumn("dbo.Employee", "TeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employee", "TeamId");
            AddForeignKey("dbo.Employee", "TeamId", "dbo.Team", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "TeamId", "dbo.Team");
            DropIndex("dbo.Employee", new[] { "TeamId" });
            AlterColumn("dbo.Employee", "TeamId", c => c.Int());
            CreateIndex("dbo.Employee", "TeamId");
            AddForeignKey("dbo.Employee", "TeamId", "dbo.Team", "Id");
        }
    }
}
