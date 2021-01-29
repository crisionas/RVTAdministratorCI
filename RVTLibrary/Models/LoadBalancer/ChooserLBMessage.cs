using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary.Models.LoadBalancer
{
    public class ChooserLBMessage
    {
        public string Gender { get; set; }
        public DateTime Birth_date { get; set; }
        public DateTime Vote_date { get; set; }
        public int Region { get; set; }
        public string IDVN { get; set; }
        public int PartyChoosed { get; set; }
    }
}
