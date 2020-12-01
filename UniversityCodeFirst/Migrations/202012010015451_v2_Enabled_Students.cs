namespace UniversityCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2_Enabled_Students : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Enabled", c => c.Boolean());
            DropColumn("dbo.People", "StudentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "StudentID", c => c.Int());
            DropColumn("dbo.People", "Enabled");
        }
    }
}
