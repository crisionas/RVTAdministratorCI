using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace RVT_DataLayer.Entities
{
    public partial class FiscData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(20)]
        public string Idnp { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Surname { get; set; }
        [Required]
        [StringLength(20)]
        public string Gender { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        [StringLength(200)]
        public string Region { get; set; }
    }
}
