using System;
using System.Collections.Generic;
using System.Text;

namespace MobileEater.Models
{
    public class Cell
    {
        public Location location { get; set; }
        public bool isPillAvailable { get; set; }
        public OccupiedBy occupiedBy { get; set; }
    }

    public class Location
    {
        public int row { get; set; }
        public int column { get; set; }
    }
}
