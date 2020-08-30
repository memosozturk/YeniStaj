using Microsoft.Build.BuildEngine;
using Microsoft.Build.Evaluation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YeniStaj.Models.Entities;

namespace YeniStaj.Abstracts
{
    public abstract class BaseEntity<T>
    {
        [Key]
        [Column(Order  = 1)]
        public T Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
    }
}