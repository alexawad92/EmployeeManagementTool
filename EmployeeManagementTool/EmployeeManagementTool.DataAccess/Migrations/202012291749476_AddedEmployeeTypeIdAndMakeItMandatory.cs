namespace EmployeeManagementTool.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmployeeTypeIdAndMakeItMandatory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employee", "EmployeeTypeId", "dbo.EmployeeType");
            DropIndex("dbo.Employee", new[] { "EmployeeTypeId" });
            AlterColumn("dbo.Employee", "EmployeeTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employee", "EmployeeTypeId");
            AddForeignKey("dbo.Employee", "EmployeeTypeId", "dbo.EmployeeType", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "EmployeeTypeId", "dbo.EmployeeType");
            DropIndex("dbo.Employee", new[] { "EmployeeTypeId" });
            AlterColumn("dbo.Employee", "EmployeeTypeId", c => c.Int());
            CreateIndex("dbo.Employee", "EmployeeTypeId");
            AddForeignKey("dbo.Employee", "EmployeeTypeId", "dbo.EmployeeType", "Id");
        }
    }
}
