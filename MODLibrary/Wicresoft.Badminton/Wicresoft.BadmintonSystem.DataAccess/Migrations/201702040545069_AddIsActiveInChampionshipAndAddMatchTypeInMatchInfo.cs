namespace Wicresoft.BadmintonSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveInChampionshipAndAddMatchTypeInMatchInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChampionshipInfoes", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.MatchInfoes", "MatchType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MatchInfoes", "MatchType");
            DropColumn("dbo.ChampionshipInfoes", "IsActive");
        }
    }
}
