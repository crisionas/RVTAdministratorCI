using RVTLibrary.Models.LoadBalancer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary.Models.Vote
{
    public class VoteCoreResponse
    {
        public VoteResponse VoteResponse { get; set; }
        public ChooserLbMessage LBMessage { get; set; }

    }
}
