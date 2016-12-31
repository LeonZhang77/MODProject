using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wicresoft.BadmintonSystem.Entity;

namespace Wicresoft.BadmintonSystem.DataAccess
{
    public class DBSource : DbContext
    {
        public DBSource()
            : base("BadmintonDBSource")
        {

        }
        public DbSet<BonusInfo> BonusInfos
        {
            get;
            set;
        }
        public DbSet<ChampionshipInfo> ChampionshipInfos
        {
            get;
            set;
        }

        public DbSet<ClubInfo> ClubInfos
        {
            get;
            set;
        }

        public DbSet<MatchInfo> MatchInfos
        {
            get;
            set;
        }

        public DbSet<MemberInfo> MemberInfos
        {
            get;
            set;
        }

        public DbSet<ScoreInfo> ScoreInfos
        {
            get;
            set;
        }

        public DbSet<MemberAndClubRelation> MemberAndClubRelations
        {
            get;
            set;
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return this.Set<T>();
        }

    }
}
