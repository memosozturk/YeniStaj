using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YeniStaj.Models.Entities
{
    [Table("Task")]
    public class Task
    {
        [Key]

        public int Taskid { get; set; }
        [DisplayName("Task Başlık")]
        public String TaskBaslik { get; set; }
        [DisplayName("Task Açıklama")]
        public String TaskAciklama { get; set; }
        [DisplayName("Task Oluşturulma Tarihi")]
        [Column(TypeName = "datetime2")]
        public DateTime TaskOlusturmaTarihi { get; set; }
        [DisplayName("Task Teslim Tarihi")]
        [Column(TypeName = "datetime2")]
        public DateTime TaskTeslimTarihi { get; set; }
        public int TaskStateId { get; set; }
        public String TaskDurumu { get; set; }

        // public Users users { get; set; }
        public int Projeid { get; set; }
        public Project project { get; set; }
    }
}