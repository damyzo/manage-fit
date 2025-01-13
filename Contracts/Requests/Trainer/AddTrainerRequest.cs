namespace Contracts.Requests.Trainer
{
    public class AddTrainerRequest(
        string name,
        string email)
    {
        public required string Name { get; set; } = name;

        public required string Email { get; set; } = email;
    }
}
