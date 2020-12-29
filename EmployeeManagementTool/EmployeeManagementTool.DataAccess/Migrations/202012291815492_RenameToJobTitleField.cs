namespace EmployeeManagementTool.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameToJobTitleField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeType", "JobTitle", c => c.String(nullable: false));
            DropColumn("dbo.EmployeeType", "EmployeeTypeEnum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeType", "EmployeeTypeEnum", c => c.String(nullable: false));
            DropColumn("dbo.EmployeeType", "JobTitle");
        }
    }
}
