using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RVT_DataLayer.Entities
{
    public partial class Block
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlockId { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        [StringLength(3000,ErrorMessage ="Hash is bigger than 3000 characters.")]
        public string Hash { get; set; }
        [Required]
        [StringLength(3000, ErrorMessage = "PreviousHash is bigger than 3000 characters.")]
        public string PreviousHash { get; set; }
        [Required]
        public int PartyId { get; set; }
        [Required]
        public int RegionId { get; set; }
        [Required]
        [StringLength(200)]
        public string Gender { get; set; }
        [Required]
        public int? YearBirth { get; set; }
        [Required]
        [StringLength(3000, ErrorMessage = "Idbd is bigger than 3000 characters.")]
        public string Idbd { get; set; }

        public virtual Party PartyNavigation { get; set; }
        public virtual Region RegionNavigation { get; set; }
    }
}
