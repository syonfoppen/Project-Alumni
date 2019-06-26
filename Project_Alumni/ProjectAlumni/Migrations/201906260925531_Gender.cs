namespace ProjectAlumni.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Gender : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "GenderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "GenderId", c => c.Int());
        }
    }
}
