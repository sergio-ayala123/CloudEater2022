namespace Shared
{
    public record LocationRecord(int row, int column);
        

    public record EnlistRequest(string host, int port);

    public class Cell
    {
        public Location location { get; set; }
        public bool isPillAvailable { get; set; }
        public OccupiedBy occupiedBy { get; set; }
    }

    public class OccupiedBy
    {
        public int id { get; set; }
        public string name { get; set; }
        public int score { get; set; }
    }
    public class Location
    {
        public int row { get; set; }
        public int column { get; set; }

    }
    public class Status 
    { 
        public string WorkerName { get; set; }
        public string State { get; set; }
        public Location Destination { get; set; }
    }
    public class SenderHostInfo
    {
        public string senderHost { get; set;}
        public string Token { get; set; }
    }
}