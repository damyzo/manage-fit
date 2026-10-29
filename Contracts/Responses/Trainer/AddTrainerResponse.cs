namespace Contracts.Responses.Trainer
{
    public class AddTrainerResponse(
        string name,
        string email,
        int type,
        Guid? gymId)
    {
        public string Name { get; } = name;

        public string Email { get; } = email;

        public int Type { get; } = type;

        public Guid? GymId { get; } = gymId;
    }
}
