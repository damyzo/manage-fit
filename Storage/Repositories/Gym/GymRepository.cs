namespace Storage.Repositories.Gym
{
    using Entities.Common;
    using Entities.Gym.Model;
    using Entities.GymLocation.Model;
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
            Gym? existing = await manageFitDbContext.Gym
                .Include(x => x.Locations)
                .FirstOrDefaultAsync(x => x.Id == gym.Id, cancellationToken);

            if (existing == null)
            {
                return GymError("Gym Not Found");
            }

            existing.Name = gym.Name;

            List<GymLocation> toRemove = existing.Locations
                .Where(x => !gym.Locations.Any(l => l.Id == x.Id))
                .ToList();

            manageFitDbContext.GymLocation.RemoveRange(toRemove);

            foreach (GymLocation incoming in gym.Locations)
            {
                GymLocation? current = existing.Locations.FirstOrDefault(x => x.Id == incoming.Id);

                if (current != null)
                {
                    current.Address = incoming.Address;
                }
                else
                {
                    existing.Locations.Add(new GymLocation { Id = Guid.NewGuid(), Address = incoming.Address, GymId = existing.Id });
                }
            }

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                return GymError(e.Message);
            }

            return new Result<Gym>(value: existing, isSuccess: true, message: "Valid Data");
        }

        public async Task<Result<Gym>> GetGym(Guid gymId, CancellationToken cancellationToken)
        {
            Gym? gym = await manageFitDbContext.Gym
                .Include(x => x.Locations)
                .Where(gym => gym.Id == gymId)
                .FirstOrDefaultAsync(cancellationToken);

            if (gym == null)
            {
                return GymError("Gym Not Found");
            }

            return new Result<Gym>(value: gym, isSuccess: true, message: "Valid Data");
        }

        public async Task<Result<IEnumerable<Gym>>> GetGyms(CancellationToken cancellationToken)
        {
            IEnumerable<Gym> gyms = await manageFitDbContext.Gym
                .Include(x => x.Locations)
                .ToListAsync(cancellationToken);

            return new Result<IEnumerable<Gym>>(value: gyms, isSuccess: true, message: "Valid Data");
        }

        public async Task<Result<Gym>> DeleteGym(Guid gymId, CancellationToken cancellationToken)
        {
            Gym? gym = await manageFitDbContext.Gym
                .Include(x => x.Locations)
                .Where(gym => gym.Id == gymId)
                .FirstOrDefaultAsync(cancellationToken);

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
                value: new Gym { Name = "", Id = Guid.Empty },
                isSuccess: false,
                message: message);
        }
    }
}
