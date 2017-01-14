using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Badminton.Models.MatchVerify
{
    public class MatchVerifyIndexModel
    {
        public MatchVerifyIndexModel()
        {
            this.WaitingForScoreList = new List<MatchModel>();
            this.WaitingForVerifyList = new List<MatchModel>();
        }
        public List<MatchModel> WaitingForVerifyList
        {
            get;
            set;
        }

        public List<MatchModel> WaitingForScoreList
        {
            get;
            set;
        }
    }
}