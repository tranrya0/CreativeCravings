namespace CreativeCravings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRequirementToRecipeName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipe", "Name", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipe", "Name", c => c.String(maxLength: 200));
        }
    }
}
