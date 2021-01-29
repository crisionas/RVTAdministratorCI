using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RVT_DataLayer.Entities
{
    public partial class Party
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartyId { get; set; }
        [Required]
        [StringLength(200)]
        public string PartyName { get; set; }
        [Required]
        [StringLength(200)]
        public string Color { get; set; }

        public virtual ICollection<Block> Blocks { get; set; }
    }
}
