namespace Contracts.Responses.Gym
{
    public class GymLocationResponse(
        Guid id,
        string address)
    {
        public string Id { get; } = id.ToString();

        public string Address { get; } = address;
    }
}
