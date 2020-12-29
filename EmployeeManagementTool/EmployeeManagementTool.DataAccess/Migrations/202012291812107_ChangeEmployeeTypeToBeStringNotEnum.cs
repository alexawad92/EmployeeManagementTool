namespace EmployeeManagementTool.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEmployeeTypeToBeStringNotEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmployeeType", "EmployeeTypeEnum", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeeType", "EmployeeTypeEnum", c => c.Int(nullable: false));
        }
    }
}
