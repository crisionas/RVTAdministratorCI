using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RVT_DataLayer.Entities
{
    public partial class VoteStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(3000)]
        public string Idvn { get; set; }
        [Required]
        [StringLength(500)]
        public string VoteState { get; set; }

        public virtual IdvnAccount IdvnAccount { get; set; }
    }
}
