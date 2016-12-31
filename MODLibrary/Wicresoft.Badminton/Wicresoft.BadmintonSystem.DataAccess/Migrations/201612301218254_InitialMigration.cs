namespace Wicresoft.BadmintonSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BonusInfoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        MatchID = c.Int(nullable: false),
                        Updated = c.Boolean(nullable: false),
                        BonusType = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        ChampionID_ID = c.Long(nullable: false),
                        MemberID_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ChampionshipInfoes", t => t.ChampionID_ID, cascadeDelete: true)
                .ForeignKey("dbo.MemberInfoes", t => t.MemberID_ID, cascadeDelete: true)
                .Index(t => t.ChampionID_ID)
                .Index(t => t.MemberID_ID);
            
            CreateTable(
                "dbo.ChampionshipInfoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ChampionType = c.Int(nullable: false),
                        CompetingType = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MemberInfoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Male = c.Boolean(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        Score = c.Int(nullable: false),
                        TestScore = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MemberAndClubRelations",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        IsCaption = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        ClubID_ID = c.Long(nullable: false),
                        MemberID_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ClubInfoes", t => t.ClubID_ID, cascadeDelete: true)
                .ForeignKey("dbo.MemberInfoes", t => t.MemberID_ID, cascadeDelete: true)
                .Index(t => t.ClubID_ID)
                .Index(t => t.MemberID_ID);
            
            CreateTable(
                "dbo.ClubInfoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MatchInfoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        MatchDate = c.DateTime(nullable: false),
                        WinnerPoints = c.Int(nullable: false),
                        LoserPoints = c.Int(nullable: false),
                        Compensation = c.Int(nullable: false),
                        Updated = c.Boolean(nullable: false),
                        Ignore = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        ChampionID_ID = c.Long(),
                        LoserID_ID = c.Long(),
                        LoserID2_ID = c.Long(),
                        WinnerID_ID = c.Long(),
                        WinnerID2_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ChampionshipInfoes", t => t.ChampionID_ID)
                .ForeignKey("dbo.MemberInfoes", t => t.LoserID_ID)
                .ForeignKey("dbo.MemberInfoes", t => t.LoserID2_ID)
                .ForeignKey("dbo.MemberInfoes", t => t.WinnerID_ID)
                .ForeignKey("dbo.MemberInfoes", t => t.WinnerID2_ID)
                .Index(t => t.ChampionID_ID)
                .Index(t => t.LoserID_ID)
                .Index(t => t.LoserID2_ID)
                .Index(t => t.WinnerID_ID)
                .Index(t => t.WinnerID2_ID);
            
            CreateTable(
                "dbo.ScoreInfoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CalculateDate = c.DateTime(nullable: false),
                        PeriodEnd = c.DateTime(nullable: false),
                        Score = c.Int(nullable: false),
                        Comment = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        MemberID_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MemberInfoes", t => t.MemberID_ID)
                .Index(t => t.MemberID_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScoreInfoes", "MemberID_ID", "dbo.MemberInfoes");
            DropForeignKey("dbo.MatchInfoes", "WinnerID2_ID", "dbo.MemberInfoes");
            DropForeignKey("dbo.MatchInfoes", "WinnerID_ID", "dbo.MemberInfoes");
            DropForeignKey("dbo.MatchInfoes", "LoserID2_ID", "dbo.MemberInfoes");
            DropForeignKey("dbo.MatchInfoes", "LoserID_ID", "dbo.MemberInfoes");
            DropForeignKey("dbo.MatchInfoes", "ChampionID_ID", "dbo.ChampionshipInfoes");
            DropForeignKey("dbo.BonusInfoes", "MemberID_ID", "dbo.MemberInfoes");
            DropForeignKey("dbo.MemberAndClubRelations", "MemberID_ID", "dbo.MemberInfoes");
            DropForeignKey("dbo.MemberAndClubRelations", "ClubID_ID", "dbo.ClubInfoes");
            DropForeignKey("dbo.BonusInfoes", "ChampionID_ID", "dbo.ChampionshipInfoes");
            DropIndex("dbo.ScoreInfoes", new[] { "MemberID_ID" });
            DropIndex("dbo.MatchInfoes", new[] { "WinnerID2_ID" });
            DropIndex("dbo.MatchInfoes", new[] { "WinnerID_ID" });
            DropIndex("dbo.MatchInfoes", new[] { "LoserID2_ID" });
            DropIndex("dbo.MatchInfoes", new[] { "LoserID_ID" });
            DropIndex("dbo.MatchInfoes", new[] { "ChampionID_ID" });
            DropIndex("dbo.MemberAndClubRelations", new[] { "MemberID_ID" });
            DropIndex("dbo.MemberAndClubRelations", new[] { "ClubID_ID" });
            DropIndex("dbo.BonusInfoes", new[] { "MemberID_ID" });
            DropIndex("dbo.BonusInfoes", new[] { "ChampionID_ID" });
            DropTable("dbo.ScoreInfoes");
            DropTable("dbo.MatchInfoes");
            DropTable("dbo.ClubInfoes");
            DropTable("dbo.MemberAndClubRelations");
            DropTable("dbo.MemberInfoes");
            DropTable("dbo.ChampionshipInfoes");
            DropTable("dbo.BonusInfoes");
        }
    }
}
