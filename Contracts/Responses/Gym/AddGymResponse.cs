namespace Contracts.Responses.Gym
{
    public class AddGymResponse(
        string name,
        string location)
    {
        public string Name { get; } = name;

        public string Location { get; } = location;
    }
}
