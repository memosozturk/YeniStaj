using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YeniStaj.Abstracts
{
    public abstract class BaseEntity4<T1, T2> :BaseEntity3<T1>
    {
         [Key]
    [Column(Order = 2)]
    public T2 Id2 { get; set; }
}
}