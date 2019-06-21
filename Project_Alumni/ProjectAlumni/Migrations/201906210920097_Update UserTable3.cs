namespace ProjectAlumni.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTable3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "AdressId", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "GenderId", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "HasBeenAccepted", c => c.Boolean());
            AlterColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "GraduationYear", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "GraduationYear", c => c.DateTime(nullable: true));
            AlterColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime(nullable: true));
            AlterColumn("dbo.AspNetUsers", "HasBeenAccepted", c => c.Boolean(nullable: true));
            AlterColumn("dbo.AspNetUsers", "GenderId", c => c.Int(nullable: true));
            AlterColumn("dbo.AspNetUsers", "AdressId", c => c.Int(nullable: true));
        }
    }
}
