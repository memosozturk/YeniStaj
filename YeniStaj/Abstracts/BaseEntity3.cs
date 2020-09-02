﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YeniStaj.Abstracts
{
    public class BaseEntity3<T>
    {
        [Key]
        [Column(Order = 1)]
        public T Id { get; set; }
    }
}