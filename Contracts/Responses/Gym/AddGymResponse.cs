namespace Contracts.Responses.Gym
{
    public class AddGymResponse(
        string name,
        IEnumerable<GymLocationResponse> locations)
    {
        public string Name { get; } = name;

        public IEnumerable<GymLocationResponse> Locations { get; } = locations;
    }
}
