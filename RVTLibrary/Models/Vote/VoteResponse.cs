using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary.Models.Vote
{
    public class VoteResponse
    {
        public bool VoteStatus { get; set; }
        public string Message { get; set; }
        public DateTime ProcessedTime { get; set; }
    }
}
