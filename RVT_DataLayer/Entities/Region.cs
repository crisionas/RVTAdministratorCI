using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RVT_DataLayer.Entities
{
    public partial class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegiondId { get; set; }
        [Required]
        [StringLength(200)]
        public string RegionName { get; set; }

        public virtual ICollection<Block> Blocks { get; set; }
        public virtual ICollection<IdvnAccount> IdvnAccounts { get; set; }
    }
}
