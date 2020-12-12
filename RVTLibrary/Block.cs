using RVTLibrary.Algoritms;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary
{
    public class Block
    {
        private IAlgorithm _algorithm = AlgorithmHelper.GetDefaultAlgorithm();
        public int ID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public int Party_Choosed { get; set; }
        public int Region_Choosed { get; set; }
        public string ChooserName { get; set; }
        public string Status { get; set; }

     
    }
}
