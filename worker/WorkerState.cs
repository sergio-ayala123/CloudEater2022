using Shared;

namespace worker
{
    public class WorkerState
    {
        public string Token { get; set; }
        public string WorkerName { get; set; }
        public Location Destination { get; set; }
    }
}
