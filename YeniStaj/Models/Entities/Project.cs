using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YeniStaj.Abstracts;
using YeniStaj.Models.IdentityModels;

namespace YeniStaj.Models.Entities
{
    [Table("Projects")]
    public class Project:BaseEntity<int>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Index("IX_ProjectUnique",IsUnique =true)]
        public String ProjeAdi { get; set; }
        public String ProjeAciklama { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime EklenmeTarihi { get; set; }
        public List<User> Users { get; set; }

    }
}