﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpata.Case.Domain.Base
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
