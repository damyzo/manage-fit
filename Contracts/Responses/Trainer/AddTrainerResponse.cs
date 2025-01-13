namespace Contracts.Responses.Trainer
{
    public class AddTrainerResponse(
        string name,
        string email)
    {
        public string Name { get; } = name;

        public string Email { get; } = email;
    }
}
