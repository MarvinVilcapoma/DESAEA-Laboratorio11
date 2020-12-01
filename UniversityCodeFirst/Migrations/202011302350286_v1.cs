namespace UniversityCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        Credits = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        InstructorID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Administrador_PersonID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.People", t => t.Administrador_PersonID)
                .Index(t => t.Administrador_PersonID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(),
                        EnrollmentDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PersonID);
            
            CreateTable(
                "dbo.OfficeAssignments",
                c => new
                    {
                        InstructorID = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.InstructorID)
                .ForeignKey("dbo.People", t => t.InstructorID)
                .Index(t => t.InstructorID);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        EnrollmentID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        Grade = c.Int(),
                        Student_PersonID = c.Int(),
                    })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Student_PersonID)
                .Index(t => t.CourseID)
                .Index(t => t.Student_PersonID);
            
            CreateTable(
                "dbo.InstructorCourses",
                c => new
                    {
                        Instructor_PersonID = c.Int(nullable: false),
                        Course_CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Instructor_PersonID, t.Course_CourseID })
                .ForeignKey("dbo.People", t => t.Instructor_PersonID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_CourseID, cascadeDelete: true)
                .Index(t => t.Instructor_PersonID)
                .Index(t => t.Course_CourseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "Student_PersonID", "dbo.People");
            DropForeignKey("dbo.Enrollments", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Courses", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Departments", "Administrador_PersonID", "dbo.People");
            DropForeignKey("dbo.OfficeAssignments", "InstructorID", "dbo.People");
            DropForeignKey("dbo.InstructorCourses", "Course_CourseID", "dbo.Courses");
            DropForeignKey("dbo.InstructorCourses", "Instructor_PersonID", "dbo.People");
            DropIndex("dbo.InstructorCourses", new[] { "Course_CourseID" });
            DropIndex("dbo.InstructorCourses", new[] { "Instructor_PersonID" });
            DropIndex("dbo.Enrollments", new[] { "Student_PersonID" });
            DropIndex("dbo.Enrollments", new[] { "CourseID" });
            DropIndex("dbo.OfficeAssignments", new[] { "InstructorID" });
            DropIndex("dbo.Departments", new[] { "Administrador_PersonID" });
            DropIndex("dbo.Courses", new[] { "DepartmentID" });
            DropTable("dbo.InstructorCourses");
            DropTable("dbo.Enrollments");
            DropTable("dbo.OfficeAssignments");
            DropTable("dbo.People");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
        }
    }
}
