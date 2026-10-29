namespace Contracts.Requests.Gym
{
    public class GymLocationRequest
    {
        public Guid? Id { get; set; }

        public required string Address { get; set; }
    }

    public class EditGymRequest(
        string name,
        IEnumerable<GymLocationRequest> locations)
    {
        public required string Name { get; set; } = name;

        public required IEnumerable<GymLocationRequest> Locations { get; set; } = locations;
    }
}
