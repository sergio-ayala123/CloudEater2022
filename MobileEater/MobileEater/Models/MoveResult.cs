using System;
using System.Collections.Generic;
using System.Text;

namespace MobileEater.Models
{
    public class MoveResult
    {
        public Location newLocation { get; set; }
        public bool ateAPill { get; set; }
    }
}
