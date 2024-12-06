namespace Storage.Repositories.WorkoutPlan
{
    using Entities.Common;
    using Entities.WorkoutPlan.Model;
    using Microsoft.EntityFrameworkCore;
    using Storage.DatabaseContext;
    using Storage.Repositories.WorkoutPlan.Interface;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class WorkoutPlanRepository(ManageFitDbContext manageFitDbContext) : IWorkoutPlanRepository
    {
        public async Task<Result<WorkoutPlan>> AddWorkoutPlan(WorkoutPlan workoutPlan, CancellationToken cancellationToken)
        {
            manageFitDbContext.WorkoutPlan.Add(workoutPlan);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<WorkoutPlan> workoutPlanOrError = new(
                    value: new WorkoutPlan { TrainerId = Guid.Empty, ClientId = Guid.Empty, Id = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return workoutPlanOrError;
            }

            return new Result<WorkoutPlan>(
                value: workoutPlan,
                isSuccess: true,
                message: "Valid Data");
        }

        public async Task<Result<WorkoutPlan>> DeleteWorkoutPlan(Guid workoutPlanId, CancellationToken cancellationToken)
        {
            WorkoutPlan? workoutPlan = await manageFitDbContext.WorkoutPlan
                .Where(workoutPlan => workoutPlan.Id == workoutPlanId)
                .FirstOrDefaultAsync(cancellationToken);

            if (workoutPlan == null)
            {
                Result<WorkoutPlan> workoutPlanOrError = new(
                    value: new WorkoutPlan { ClientId = Guid.Empty, TrainerId = Guid.Empty, Id = Guid.Empty },
                    isSuccess: false,
                    message: "Workout Plan Not Found");

                return workoutPlanOrError;
            }

            manageFitDbContext.WorkoutPlan.Remove(workoutPlan);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<WorkoutPlan> workoutPlanOrError = new(
                    value: new WorkoutPlan { ClientId = Guid.Empty, TrainerId = Guid.Empty, Id = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return workoutPlanOrError;
            }

            return new Result<WorkoutPlan>(
                value: workoutPlan,
                isSuccess: true,
                message: "Client deleted");
        }

        public async Task<Result<WorkoutPlan>> GetWorkoutPlan(Guid workoutPlanId, CancellationToken cancellationToken)
        {
            WorkoutPlan? workoutPlan = await manageFitDbContext.WorkoutPlan
                .Where(workoutPlan => workoutPlan.Id == workoutPlanId)
                .FirstOrDefaultAsync(cancellationToken);

            if (workoutPlan == null)
            {
                Result<WorkoutPlan> workoutPlanOrError = new(
                    value: new WorkoutPlan { ClientId = Guid.Empty, TrainerId = Guid.Empty, Id = Guid.Empty },
                    isSuccess: false,
                    message: "Workout Plan Not Found");

                return workoutPlanOrError;
            }

            return new Result<WorkoutPlan>(
                value: workoutPlan,
                isSuccess: true,
                message: "Valid Data");
        }

        public async Task<Result<IEnumerable<WorkoutPlan>>> GetWorkoutPlansByTrainerIdAndClientId(Guid trainerId, Guid clientId, CancellationToken cancellationToken)
        {
            IEnumerable<WorkoutPlan> workoutPlans = await manageFitDbContext.WorkoutPlan
                .Where(workoutPlan => workoutPlan.TrainerId == trainerId && workoutPlan.ClientId == clientId)
                .ToListAsync(cancellationToken);

            return new Result<IEnumerable<WorkoutPlan>>(value: workoutPlans, isSuccess: true, message: "Valid Data");
        }

        public async Task<Result<WorkoutPlan>> UpdateWorkoutPlan(WorkoutPlan workoutPlan, CancellationToken cancellationToken)
        {
            manageFitDbContext.WorkoutPlan.Update(workoutPlan);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<WorkoutPlan> workoutPlanOrError = new(
                    value: new WorkoutPlan { ClientId = Guid.Empty, TrainerId = Guid.Empty, Id = Guid.Empty },
                    isSuccess: false,
                    message: e.Message);

                return workoutPlanOrError;
            }

            return new Result<WorkoutPlan>(
                value: workoutPlan,
                isSuccess: true,
                message: "Valid Data");
        }
    }
}
