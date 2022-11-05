﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ProductCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DefaultQuantity { get; set; }
    }
}
