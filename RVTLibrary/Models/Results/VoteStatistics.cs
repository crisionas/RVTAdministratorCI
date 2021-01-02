using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary.Models.Results
{
    public class VoteStatistics
    {
        public int IDParty { get; set; }
        public string Name { get; set; }
        public int Votes { get; set; }
        public string Color { get; set; }
    }
}
