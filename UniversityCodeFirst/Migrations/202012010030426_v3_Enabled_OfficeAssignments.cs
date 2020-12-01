namespace UniversityCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3_Enabled_OfficeAssignments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfficeAssignments", "Enabled", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OfficeAssignments", "Enabled");
        }
    }
}
