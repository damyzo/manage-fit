namespace Contracts.Responses.Client
{
    public class EditClientResponse(
        string name,
        double weight,
        double height,
        string email)
    {
        public string Name { get; } = name;

        public double Weight { get; } = weight;

        public double Height { get; } = height;

        public string Email { get; } = email;
    }
}
