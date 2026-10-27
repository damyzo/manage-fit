namespace Contracts.Requests.Exercise
{
    public class AddExerciseRequest(
        string name,
        string description,
        string videoUrl)
    {
        public string Name { get; } = name;

        public string Description { get; } = description;

        public string VideoUrl { get; } = videoUrl;
    }
}
