﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Number { get; set; }
    }
}
