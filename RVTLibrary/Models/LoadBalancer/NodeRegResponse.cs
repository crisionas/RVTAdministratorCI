using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary.Models.LoadBalancer
{
    public class NodeRegResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public DateTime ProcessedTime { get; set; }
        public string IDVN { get; set; }
        public string VnPassword { get; set; }
    }
}
