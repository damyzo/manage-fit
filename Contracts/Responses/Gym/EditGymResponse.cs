namespace Contracts.Responses.Gym
{
    public class EditGymResponse(
        string name,
        string location)
    {
        public string Name { get; } = name;

        public string Location { get; } = location;
    }
}
