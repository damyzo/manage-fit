namespace Contracts.Responses.Client
{
    public class GetClientsResponse(
        string name,
        double weight,
        double height,
        string email,
        DateOnly? dateOfBirth,
        Guid id,
        Guid? gymId = null,
        IEnumerable<Contracts.Responses.Gym.GymLocationResponse>? gymLocations = null)
    {
        public string Name { get; } = name;

        public double Weight { get; } = weight;

        public double Height { get; } = height;

        public string Email { get; } = email;

        public DateOnly? DateOfBirth { get; } = dateOfBirth;

        public string Id { get; } = id.ToString();

        public Guid? GymId { get; } = gymId;

        public IEnumerable<Contracts.Responses.Gym.GymLocationResponse> GymLocations { get; } = gymLocations ?? [];
    }
}
