using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary.Models.Results
{
    public class StatisticsResponse
    {
        public string Name { get; set; }
        public List<AgeStatistics> AgeVoters { get; set; }
        public int Voters { get; set; }
        public int Population { get; set; }
        public DateTime Time { get; set; }
        public GenderStatistic GenderStatistics { get; set; }
    }
}
