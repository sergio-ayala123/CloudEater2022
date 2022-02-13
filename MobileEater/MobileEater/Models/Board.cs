using System;
using System.Collections.Generic;
using System.Text;

namespace MobileEater.Models
{
    public class Board
    {
        public Location location { get; set; }
        public bool isPillAvailable { get; set; }
        public Player occupiedBy { get; set; }
    }

    public class Location
    {
        public int row { get; set; }
        public int column { get; set; }
    }
}
