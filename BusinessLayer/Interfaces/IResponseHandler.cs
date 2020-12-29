using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IResponseHandler
    {
        public void PrepareVoteResponse(string content);
    }
}
