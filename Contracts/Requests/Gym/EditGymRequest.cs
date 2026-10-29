namespace Contracts.Requests.Gym
{
    public class EditGymRequest(
        string name,
        string location)
    {
        public required string Name { get; set; } = name;

        public required string Location { get; set; } = location;
    }
}
