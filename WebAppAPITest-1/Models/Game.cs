﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppAPITest.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}