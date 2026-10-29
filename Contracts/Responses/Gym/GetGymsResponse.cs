namespace Contracts.Responses.Gym
{
    public class GetGymsResponse(
        string name,
        string location,
        Guid id)
    {
        public string Name { get; } = name;

        public string Location { get; } = location;

        public string Id { get; } = id.ToString();
    }
}
