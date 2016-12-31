using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.BadmintonSystem.Entity
{
    public class ChampionshipInfo : BaseEntity
    {
        [Required]
        public virtual String Title
        {
            get;
            set;
        }

        public virtual DateTime StartDate
        {
            get;
            set;
        }

        public virtual DateTime EndDate
        {
            get;
            set;
        }

        [Required]
        public virtual ChampionType ChampionType
        {
            get;
            set;
        }

        [Required]
        public virtual CompetingType CompetingType
        {
            get;
            set;
        }
    }
}
