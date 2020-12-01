namespace UniversityCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1StudentStudentID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "StudentID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "StudentID");
        }
    }
}
