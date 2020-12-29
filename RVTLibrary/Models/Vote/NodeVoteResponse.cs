using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary.Models.Vote
{
    public class NodeVoteResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public DateTime ProcessedTime { get; set; }
        public Block block { get; set; }
        public string IDVN { get; set; }
    }
}
