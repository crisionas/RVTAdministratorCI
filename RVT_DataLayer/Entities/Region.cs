using System;
using System.Collections.Generic;

#nullable disable

namespace RVT_DataLayer.Entities
{
    public partial class Region
    {
        public Region()
        {
            Blocks = new HashSet<Block>();
            IdvnAccounts = new HashSet<IdvnAccount>();
        }

        public int Idreg { get; set; }
        public string Region1 { get; set; }

        public virtual ICollection<Block> Blocks { get; set; }
        public virtual ICollection<IdvnAccount> IdvnAccounts { get; set; }
    }
}
