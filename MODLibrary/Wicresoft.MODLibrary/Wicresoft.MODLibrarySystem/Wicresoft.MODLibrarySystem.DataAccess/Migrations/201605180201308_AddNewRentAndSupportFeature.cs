namespace Wicresoft.MODLibrarySystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewRentAndSupportFeature : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DelayRecords",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Comments = c.String(maxLength: 1024),
                        CreateTime = c.DateTime(nullable: false),
                        BorrowAndReturnRecordInfo_ID = c.Long(nullable: false),
                        UserInfo_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BorrowAndReturnRecordInfoes", t => t.BorrowAndReturnRecordInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_ID, cascadeDelete: true)
                .Index(t => t.BorrowAndReturnRecordInfo_ID)
                .Index(t => t.UserInfo_ID);
            
            CreateTable(
                "dbo.ProcessRecords",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        Comments = c.String(maxLength: 1024),
                        CreateTime = c.DateTime(nullable: false),
                        BorrowAndReturnRecordInfo_ID = c.Long(nullable: false),
                        UserInfo_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BorrowAndReturnRecordInfoes", t => t.BorrowAndReturnRecordInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_ID, cascadeDelete: true)
                .Index(t => t.BorrowAndReturnRecordInfo_ID)
                .Index(t => t.UserInfo_ID);
            
            CreateTable(
                "dbo.SupportORAgainsts",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        BookInfo_ID = c.Long(nullable: false),
                        UserInfo_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BookInfoes", t => t.BookInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_ID, cascadeDelete: true)
                .Index(t => t.BookInfo_ID)
                .Index(t => t.UserInfo_ID);
            
            AddColumn("dbo.BookInfoes", "Summary", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SupportORAgainsts", "UserInfo_ID", "dbo.UserInfoes");
            DropForeignKey("dbo.SupportORAgainsts", "BookInfo_ID", "dbo.BookInfoes");
            DropForeignKey("dbo.ProcessRecords", "UserInfo_ID", "dbo.UserInfoes");
            DropForeignKey("dbo.ProcessRecords", "BorrowAndReturnRecordInfo_ID", "dbo.BorrowAndReturnRecordInfoes");
            DropForeignKey("dbo.DelayRecords", "UserInfo_ID", "dbo.UserInfoes");
            DropForeignKey("dbo.DelayRecords", "BorrowAndReturnRecordInfo_ID", "dbo.BorrowAndReturnRecordInfoes");
            DropIndex("dbo.SupportORAgainsts", new[] { "UserInfo_ID" });
            DropIndex("dbo.SupportORAgainsts", new[] { "BookInfo_ID" });
            DropIndex("dbo.ProcessRecords", new[] { "UserInfo_ID" });
            DropIndex("dbo.ProcessRecords", new[] { "BorrowAndReturnRecordInfo_ID" });
            DropIndex("dbo.DelayRecords", new[] { "UserInfo_ID" });
            DropIndex("dbo.DelayRecords", new[] { "BorrowAndReturnRecordInfo_ID" });
            DropColumn("dbo.BookInfoes", "Summary");
            DropTable("dbo.SupportORAgainsts");
            DropTable("dbo.ProcessRecords");
            DropTable("dbo.DelayRecords");
        }
    }
}
