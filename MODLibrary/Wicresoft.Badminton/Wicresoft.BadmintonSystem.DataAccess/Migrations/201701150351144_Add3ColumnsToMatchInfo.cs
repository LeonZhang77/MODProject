namespace Wicresoft.BadmintonSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add3ColumnsToMatchInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MatchInfoes", "VerifyDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MatchInfoes", "Verified", c => c.Boolean(nullable: false));
            AddColumn("dbo.MatchInfoes", "InputPersonID_ID", c => c.Long());
            CreateIndex("dbo.MatchInfoes", "InputPersonID_ID");
            AddForeignKey("dbo.MatchInfoes", "InputPersonID_ID", "dbo.MemberInfoes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchInfoes", "InputPersonID_ID", "dbo.MemberInfoes");
            DropIndex("dbo.MatchInfoes", new[] { "InputPersonID_ID" });
            DropColumn("dbo.MatchInfoes", "InputPersonID_ID");
            DropColumn("dbo.MatchInfoes", "Verified");
            DropColumn("dbo.MatchInfoes", "VerifyDate");
        }
    }
}
