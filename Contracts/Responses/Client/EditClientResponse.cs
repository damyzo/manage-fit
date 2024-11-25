namespace Contracts.Responses.Client
{
    public class EditClientResponse(
        string name,
        float weight,
        float height,
        string email)
    {
        public string Name { get; } = name;

        public float Weight { get; } = weight;

        public float Height { get; } = height;

        public string Email { get; } = email;
    }
}
