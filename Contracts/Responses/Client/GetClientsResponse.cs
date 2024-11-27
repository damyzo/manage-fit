namespace Contracts.Responses.Client
{
    public class GetClientsResponse(
        string name,
        float weight,
        float height,
        string email,
        Guid id)
    {
        public string Name { get; } = name;
        public float Weight { get; } = weight;
        public float Height { get; } = height;
        public string Email { get; } = email;
        public string Id { get; } = id.ToString();
    }
}
