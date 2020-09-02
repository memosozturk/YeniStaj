using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YeniStaj.Models.Entities
{
    [Table("TaskState")]
    public class TaskState
    {
        [Key]
        public int TaskStateId { get; set; }
        public String TaskDurumu { get; set; }
    }
}