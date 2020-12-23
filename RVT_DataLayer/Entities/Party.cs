using System;
using System.Collections.Generic;

#nullable disable

namespace RVT_DataLayer.Entities
{
    public partial class Party
    {
        public Party()
        {
            Blocks = new HashSet<Block>();
        }

        public int Idpart { get; set; }
        public string Party1 { get; set; }
        public string Color { get; set; }

        public virtual ICollection<Block> Blocks { get; set; }
    }
}
