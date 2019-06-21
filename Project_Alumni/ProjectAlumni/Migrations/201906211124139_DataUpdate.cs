namespace ProjectAlumni.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "GraduationYear", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "GraduationYear", c => c.DateTime());
        }
    }
}
