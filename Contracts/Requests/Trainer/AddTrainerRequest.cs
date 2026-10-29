namespace Contracts.Requests.Trainer
{
    public class AddTrainerRequest(
        string name,
        string email,
        int type = 0,
        string? gymName = null,
        IEnumerable<string>? gymLocations = null)
    {
        public required string Name { get; set; } = name;

        public required string Email { get; set; } = email;

        public int Type { get; set; } = type;

        public string? GymName { get; set; } = gymName;

        public IEnumerable<string>? GymLocations { get; set; } = gymLocations;
    }
}
