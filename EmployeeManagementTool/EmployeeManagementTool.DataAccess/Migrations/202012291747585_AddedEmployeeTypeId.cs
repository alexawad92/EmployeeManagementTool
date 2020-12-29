namespace EmployeeManagementTool.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmployeeTypeId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Employee", name: "EmployeeType_Id", newName: "EmployeeTypeId");
            RenameIndex(table: "dbo.Employee", name: "IX_EmployeeType_Id", newName: "IX_EmployeeTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Employee", name: "IX_EmployeeTypeId", newName: "IX_EmployeeType_Id");
            RenameColumn(table: "dbo.Employee", name: "EmployeeTypeId", newName: "EmployeeType_Id");
        }
    }
}
