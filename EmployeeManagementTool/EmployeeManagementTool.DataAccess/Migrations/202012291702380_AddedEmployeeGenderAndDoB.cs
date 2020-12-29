namespace EmployeeManagementTool.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmployeeGenderAndDoB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employee", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "Gender");
            DropColumn("dbo.Employee", "DateOfBirth");
        }
    }
}
