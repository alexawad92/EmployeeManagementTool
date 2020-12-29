namespace EmployeeManagementTool.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmployeeTypeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeTypeEnum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Employee", "EmployeeType_Id", c => c.Int());
            CreateIndex("dbo.Employee", "EmployeeType_Id");
            AddForeignKey("dbo.Employee", "EmployeeType_Id", "dbo.EmployeeType", "Id");
            DropColumn("dbo.Employee", "EmployeeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "EmployeeType", c => c.Int(nullable: false));
            DropForeignKey("dbo.Employee", "EmployeeType_Id", "dbo.EmployeeType");
            DropIndex("dbo.Employee", new[] { "EmployeeType_Id" });
            DropColumn("dbo.Employee", "EmployeeType_Id");
            DropTable("dbo.EmployeeType");
        }
    }
}
