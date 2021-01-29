using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RVT_DataLayer.Entities
{
    public partial class IdvnAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(3000, ErrorMessage = "Idvn is bigger than 3000 characters.")]
        public string Idvn { get; set; }
        [Required]
        [StringLength(500)]
        public string VnPassword { get; set; }
        [Required]
        [StringLength(500)]
        public string Email { get; set; }
        [Required]
        [StringLength(500)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(20)]
        public string Gender { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int RegionId { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        [StringLength(500)]
        public string IpAddress { get; set; }
        [Required]
        [StringLength(500)]
        public string StatusNumber { get; set; }

        public virtual VoteStatus VoteStatus { get; set; }
        public virtual Region Region { get; set; }
    }
}
