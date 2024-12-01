namespace Contracts.Responses.Exercise
{
    public class GetExercisesResponse(
        string name,
        string description,
        string videoUrl,
        Guid id)
    {
        public string Name { get; } = name;

        public string Description { get; } = description;

        public string VideoUrl { get; } = videoUrl;

        public string Id { get; } = id.ToString();
    }
}
