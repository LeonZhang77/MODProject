namespace Wicresoft.MODLibrarySystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.UserInfoes",
            //    c => new
            //        {
            //            ID = c.Long(nullable: false, identity: true),
            //            DisplayName = c.String(),
            //            RealName = c.String(),
            //            LoginName = c.String(),
            //            Password = c.String(),
            //            IsShow = c.Boolean(nullable: false),
            //            CreateTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.UserInfoes");
        }
    }
}
