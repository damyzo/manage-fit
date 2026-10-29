namespace Storage.Repositories.Gym
{
    using Entities.Common;
    using Entities.Gym.Model;
    using Microsoft.EntityFrameworkCore;
    using Storage.DatabaseContext;
    using Storage.Repositories.Gym.Interface;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GymRepository(ManageFitDbContext manageFitDbContext) : IGymRepository
    {
        public async Task<Result<Gym>> AddGym(Gym gym, CancellationToken cancellationToken)
        {
            manageFitDbContext.Gym.Add(gym);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                return GymError(e.Message);
            }

            return new Result<Gym>(value: gym, isSuccess: true, message: "Valid Data");
        }

        public async Task<Result<Gym>> UpdateGym(Gym gym, CancellationToken cancellationToken)
        {
            manageFitDbContext.Gym.Update(gym);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                return GymError(e.Message);
            }

            return new Result<Gym>(value: gym, isSuccess: true, message: "Valid Data");
        }

        public async Task<Result<Gym>> GetGym(Guid gymId, CancellationToken cancellationToken)
        {
            Gym? gym = await manageFitDbContext.Gym.Where(gym => gym.Id == gymId).FirstOrDefaultAsync(cancellationToken);

            if (gym == null)
            {
                return GymError("Gym Not Found");
            }

            return new Result<Gym>(value: gym, isSuccess: true, message: "Valid Data");
        }

        public async Task<Result<IEnumerable<Gym>>> GetGyms(CancellationToken cancellationToken)
        {
            IEnumerable<Gym> gyms = await manageFitDbContext.Gym.ToListAsync(cancellationToken);

            return new Result<IEnumerable<Gym>>(value: gyms, isSuccess: true, message: "Valid Data");
        }

        public async Task<Result<Gym>> DeleteGym(Guid gymId, CancellationToken cancellationToken)
        {
            Gym? gym = await manageFitDbContext.Gym.Where(gym => gym.Id == gymId).FirstOrDefaultAsync(cancellationToken);

            if (gym == null)
            {
                return GymError("Gym Not Found");
            }

            manageFitDbContext.Gym.Remove(gym);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                return GymError(e.Message);
            }

            return new Result<Gym>(value: gym, isSuccess: true, message: "Gym deleted");
        }

        private static Result<Gym> GymError(string message)
        {
            return new Result<Gym>(
                value: new Gym { Name = "", Location = "", Id = Guid.Empty },
                isSuccess: false,
                message: message);
        }
    }
}
