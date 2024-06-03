namespace DocTruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tb_Viewed", new[] { "User_Id" });
            DropColumn("dbo.tb_Viewed", "UserId");
            RenameColumn(table: "dbo.tb_Viewed", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.tb_Viewed", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.tb_Viewed", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.tb_Viewed", new[] { "UserId" });
            AlterColumn("dbo.tb_Viewed", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.tb_Viewed", name: "UserId", newName: "User_Id");
            AddColumn("dbo.tb_Viewed", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_Viewed", "User_Id");
        }
    }
}
