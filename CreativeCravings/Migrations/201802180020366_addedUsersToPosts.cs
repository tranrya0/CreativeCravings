namespace CreativeCravings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUsersToPosts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "AuthorID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Post", "AuthorID");
        }
    }
}
