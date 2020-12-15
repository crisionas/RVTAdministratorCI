using BusinessLayer.Interfaces;
using BusinessLayer.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class BusinessManager
    {
        public IUser GetUser()
        {
            return new UserLevel();
        }
        public IStatistics GetResults()
        {
            return new StatisticsLevel();
        }
    }
}
