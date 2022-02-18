namespace Shared
{
    public record Location(int row, int column);
        

    public record EnlistRequest(string host, int port);
}