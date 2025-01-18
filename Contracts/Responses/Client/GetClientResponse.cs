namespace Contracts.Responses.Client
{
    public class GetClientResponse(
        string name,
        double weight,
        double height,
        string email,
        Guid id)
    {
        public string Name { get; } = name;

        public double Weight { get; } = weight;

        public double Height { get; } = height;

        public string Email { get; } = email;

        public string Id { get; } = id.ToString();
    }
}
