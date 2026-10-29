namespace Contracts.Responses.Gym
{
    public class GetGymsResponse(
        string name,
        IEnumerable<GymLocationResponse> locations,
        Guid id)
    {
        public string Name { get; } = name;

        public IEnumerable<GymLocationResponse> Locations { get; } = locations;

        public string Id { get; } = id.ToString();
    }
}
