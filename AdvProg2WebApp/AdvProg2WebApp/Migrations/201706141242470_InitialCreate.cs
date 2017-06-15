namespace AdvProg2WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserNameId = c.String(nullable: false, maxLength: 128),
                        Losses = c.Int(nullable: false),
                        Victories = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserNameId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
