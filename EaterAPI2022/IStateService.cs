namespace EaterAPI2022
{
    public interface IStateService
    {
        public string Token { get; set; }
        public int EatenPills { get; set; }
    }
    public class StateService: IStateService
    {
        public string Token { get; set; }
        public int EatenPills { get; set; }
    }
}
