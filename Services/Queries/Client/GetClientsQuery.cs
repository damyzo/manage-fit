
namespace Services.Queries.Client
{
    using Entities.Client.Model;
    using Entities.Common;
    using MediatR;
    using Storage.Repositories.Client.Interface;
    using Storage.Repositories.Trainer.Interface;
    using Entities.Trainer.Model;
    using System.Collections.Generic;

    using System.Threading;
    using System.Threading.Tasks;

    public class GetClientsQuery(string trainerEmail) : IRequest<Result<IEnumerable<Client>>> 
    {
        public string TrainerEmail { get; } = trainerEmail;
    }

    public class GetClientsQueryHandler(
        IClientRepository clientRepository,
        ITrainerRepository trainerRepository) : IRequestHandler<GetClientsQuery, Result<IEnumerable<Client>>>
    {
        public async Task<Result<IEnumerable<Client>>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            Result<Trainer> trainerResult = await trainerRepository.GetTrainerByEmail(request.TrainerEmail, cancellationToken);

            if (!trainerResult.IsSuccess)
            {
                return new Result<IEnumerable<Client>>(value: [], isSuccess: false, message: trainerResult.Message);
            }

            return await clientRepository.GetClients(trainerResult.Value.Id, cancellationToken);
        }
    }

}
