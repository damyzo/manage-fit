namespace Storage.Repositories.Client.Interface
{
    using Entities.Client.Model;
    using Services.Common;

    public interface IClientRepository
    {
        public Task<Result<Client>> GetClient(Guid clientUid);
        public Task<Result<IEnumerable<Client>>> GetClients();

    }
}
