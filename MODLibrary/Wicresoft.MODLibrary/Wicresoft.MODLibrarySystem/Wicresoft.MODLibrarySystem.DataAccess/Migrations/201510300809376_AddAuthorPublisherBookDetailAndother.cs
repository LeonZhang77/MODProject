namespace Wicresoft.MODLibrarySystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthorPublisherBookDetailAndother : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorInfoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        AuthorName = c.String(nullable: false),
                        AuthorIntroduction = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BookAndAuthorRelations",
                c => new
                    {
                        Book_ID = c.Long(nullable: false),
                        Author_ID = c.Long(nullable: false),
                        OrderIndex = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_ID, t.Author_ID })
                .ForeignKey("dbo.AuthorInfoes", t => t.Author_ID, cascadeDelete: false)
                .ForeignKey("dbo.BookInfoes", t => t.Book_ID, cascadeDelete: false)
                .Index(t => t.Book_ID)
                .Index(t => t.Author_ID);
            
            CreateTable(
                "dbo.PublisherInfoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PublisherName = c.String(nullable: false),
                        PublisherIntroduction = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BookDetailInfoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        Storage_Time = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        BookInfo_ID = c.Long(nullable: false),
                        UserInfo_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BookInfoes", t => t.BookInfo_ID, cascadeDelete: false)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_ID, cascadeDelete: false)
                .Index(t => t.BookInfo_ID)
                .Index(t => t.UserInfo_ID);
            
            CreateTable(
                "dbo.BorrowAndReturnRecordInfoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Borrow_Date = c.DateTime(nullable: false),
                        Forcast_Date = c.DateTime(nullable: false),
                        Return_Date = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        BookDetailInfo_ID = c.Long(nullable: false),
                        UserInfo_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BookDetailInfoes", t => t.BookDetailInfo_ID, cascadeDelete: false)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_ID, cascadeDelete: false)
                .Index(t => t.BookDetailInfo_ID)
                .Index(t => t.UserInfo_ID);
            
            AddColumn("dbo.BookInfoes", "ISBN", c => c.String(nullable: false));
            AddColumn("dbo.BookInfoes", "Publish_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.BookInfoes", "Avaliable_Inventory", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BookInfoes", "Max_Inventory", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BookInfoes", "Price_Inventory", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.BookInfoes", "PublisherInfo_ID", c => c.Long(nullable: true));
            AddColumn("dbo.UserInfoes", "Floor", c => c.Int(nullable: false));
            AddColumn("dbo.UserInfoes", "PM", c => c.String());
            AddColumn("dbo.UserInfoes", "Team", c => c.String());
            AddColumn("dbo.UserInfoes", "Chinese_Name", c => c.String());
            AddColumn("dbo.UserInfoes", "Wechat", c => c.String());
            AddColumn("dbo.UserInfoes", "Grade", c => c.Int(nullable: false));
            AddColumn("dbo.UserInfoes", "Late_point", c => c.Int(nullable: false));
            AddColumn("dbo.UserInfoes", "Remark", c => c.String());
            CreateIndex("dbo.BookInfoes", "PublisherInfo_ID");
            AddForeignKey("dbo.BookInfoes", "PublisherInfo_ID", "dbo.PublisherInfoes", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowAndReturnRecordInfoes", "UserInfo_ID", "dbo.UserInfoes");
            DropForeignKey("dbo.BorrowAndReturnRecordInfoes", "BookDetailInfo_ID", "dbo.BookDetailInfoes");
            DropForeignKey("dbo.BookDetailInfoes", "UserInfo_ID", "dbo.UserInfoes");
            DropForeignKey("dbo.BookDetailInfoes", "BookInfo_ID", "dbo.BookInfoes");
            DropForeignKey("dbo.BookAndAuthorRelations", "Book_ID", "dbo.BookInfoes");
            DropForeignKey("dbo.BookInfoes", "PublisherInfo_ID", "dbo.PublisherInfoes");
            DropForeignKey("dbo.BookAndAuthorRelations", "Author_ID", "dbo.AuthorInfoes");
            DropIndex("dbo.BorrowAndReturnRecordInfoes", new[] { "UserInfo_ID" });
            DropIndex("dbo.BorrowAndReturnRecordInfoes", new[] { "BookDetailInfo_ID" });
            DropIndex("dbo.BookDetailInfoes", new[] { "UserInfo_ID" });
            DropIndex("dbo.BookDetailInfoes", new[] { "BookInfo_ID" });
            DropIndex("dbo.BookInfoes", new[] { "PublisherInfo_ID" });
            DropIndex("dbo.BookAndAuthorRelations", new[] { "Author_ID" });
            DropIndex("dbo.BookAndAuthorRelations", new[] { "Book_ID" });
            DropColumn("dbo.UserInfoes", "Remark");
            DropColumn("dbo.UserInfoes", "Late_point");
            DropColumn("dbo.UserInfoes", "Grade");
            DropColumn("dbo.UserInfoes", "Wechat");
            DropColumn("dbo.UserInfoes", "Chinese_Name");
            DropColumn("dbo.UserInfoes", "Team");
            DropColumn("dbo.UserInfoes", "PM");
            DropColumn("dbo.UserInfoes", "Floor");
            DropColumn("dbo.BookInfoes", "PublisherInfo_ID");
            DropColumn("dbo.BookInfoes", "Price_Inventory");
            DropColumn("dbo.BookInfoes", "Max_Inventory");
            DropColumn("dbo.BookInfoes", "Avaliable_Inventory");
            DropColumn("dbo.BookInfoes", "Publish_Date");
            DropColumn("dbo.BookInfoes", "ISBN");
            DropTable("dbo.BorrowAndReturnRecordInfoes");
            DropTable("dbo.BookDetailInfoes");
            DropTable("dbo.PublisherInfoes");
            DropTable("dbo.BookAndAuthorRelations");
            DropTable("dbo.AuthorInfoes");
        }
    }
}
