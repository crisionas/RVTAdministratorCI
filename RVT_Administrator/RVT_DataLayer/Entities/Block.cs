using System;
using System.Collections.Generic;

#nullable disable

namespace RVT_DataLayer.Entities
{
    public partial class Block
    {
        public int BlockId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public int PartyChoosed { get; set; }
        public int RegionChoosed { get; set; }
        public string Gender { get; set; }
        public int? YearBirth { get; set; }
    }
}
