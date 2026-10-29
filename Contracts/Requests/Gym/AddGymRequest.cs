namespace Contracts.Requests.Gym
{
    public class AddGymRequest(
        string name,
        IEnumerable<string> locations)
    {
        public required string Name { get; set; } = name;

        public required IEnumerable<string> Locations { get; set; } = locations;
    }
}
