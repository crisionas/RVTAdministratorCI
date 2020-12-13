using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary.Models.Results
{
    public class ResultsResponse
    {   
        public string Name { get; set; }
        public List<VoteStatistics> TotalVotes { get; set; }
        public int Votants { get; set; }
        public DateTime Time { get; set; }
        public int Population { get; set; }
        public int Pending { get; set; }
        public GenderStatistic GenderStatistics { get; set; }
    }
}
