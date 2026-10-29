namespace Contracts.Responses.Gym
{
    public class DeleteGymResponse(
        string name,
        string location)
    {
        public string Name { get; } = name;

        public string Location { get; } = location;
    }
}
