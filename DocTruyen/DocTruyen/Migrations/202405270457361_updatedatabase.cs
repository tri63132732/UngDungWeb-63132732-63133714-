namespace DocTruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Viewed", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Viewed", "Comment");
        }
    }
}
