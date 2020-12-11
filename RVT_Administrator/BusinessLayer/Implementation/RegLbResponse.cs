using System;

namespace BusinessLayer.Implementation
{
    public class RegLbResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public DateTime ProcessedTime { get; set; }
        public string IDVN { get; set; }
        public string VnPassword { get; set; }
    }
}