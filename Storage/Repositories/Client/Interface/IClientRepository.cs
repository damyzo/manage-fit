namespace Storage.Repositories.Client.Interface
{
    using Entities.Client.Model;
    using Entities.Common;

    public interface IClientRepository
    {
        public Task<Result<Client>> GetClient(Guid clientUid, CancellationToken cancellationToken);
        public Task<Result<IEnumerable<Client>>> GetClients(CancellationToken cancellationToken);

        public Task<Result<Client>> AddClient(Client client, CancellationToken cancellationToken);

        public Task<Result<Client>> UpdateClient(Client client, CancellationToken cancellationToken);
    }
}
