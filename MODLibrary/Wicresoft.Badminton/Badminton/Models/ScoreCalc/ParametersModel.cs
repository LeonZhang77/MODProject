﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Badminton.Models.ScoreCalc
{
    public class ParametersModel
    {
        public ParametersModel()
        {
            this.SingleIngore = 200;
            this.DoubleIngore = 150;
            this.SingleWin = 5;
            this.SingleLose = 2;
            this.DoubleWin = 4;
            this.DoubleLose = 2;

            this.RankBonus1 = 10;
            this.Rank1End = 5;

            this.RankBonus2 = 8;
            this.Rank2End = 10;

            this.RankBonus3 = 6;
            this.Rank3End = 15;

            this.RankBonus4 = 4;
            this.Rank4End = 30;

            this.FinalWin = 25;
            this.FinalLose = 10;

            this.Top4Win = 20;
            this.Top4Lose = 5;

            this.Top8Win = 15;
            this.Top8Lose = 0;
        }

        public long SingleIngore
        {
            get;
            set;
        }

        public long DoubleIngore
        {
            get;
            set;
        }
        public long SingleWin
        {
            get;
            set;
        }

        public long DoubleWin
        {
            get;
            set;
        }

        public long SingleLose
        {
            get;
            set;
        }

        public long DoubleLose
        {
            get;
            set;
        }

        public long RankBonus1
        {
            get;
            set;
        }

        public long RankBonus2
        {
            get;
            set;
        }

        public long RankBonus3
        {
            get;
            set;
        }

        public long RankBonus4
        {
            get;
            set;
        }
           

        public long Rank1End
        {
            get;
            set;
        }

        public long Rank2End
        {
            get;
            set;
        }

        public long Rank3End
        {
            get;
            set;
        }
        public long Rank4End
        {
            get;
            set;
        }

        public long FinalWin
        {
            get;
            set;
        }
        public long FinalLose
        {
            get;
            set;
        }

        public long Top4Win
        {
            get;
            set;
        }

        public long Top4Lose
        {
            get;
            set;
        }

        public long Top8Win
        {
            get;
            set;
        }

        public long Top8Lose
        {
            get;
            set;
        }
    }
}