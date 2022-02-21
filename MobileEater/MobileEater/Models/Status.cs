using System;
using System.Collections.Generic;
using System.Text;

namespace MobileEater.Models
{
    public class Status
    {
        public string WorkerName { get; set; }
        public string State { get; set; }
        public Location Destination { get; set; }
    }
}
