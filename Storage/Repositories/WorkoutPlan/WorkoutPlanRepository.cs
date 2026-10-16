namespace Storage.Repositories.WorkoutPlan
{
    using Entities.Common;
    using Entities.WorkoutPlan.Model;
    using Entities.WorkoutPlanExercise.Model;
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
                    value: new WorkoutPlan { Name = string.Empty, TrainerId = Guid.Empty, Id = Guid.Empty },
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
                .Include(x => x.WorkoutPlanClient)
                .Include(x => x.WorkoutPlanExercise)
                .Where(workoutPlan => workoutPlan.Id == workoutPlanId)
                .FirstOrDefaultAsync(cancellationToken);

            if (workoutPlan == null)
            {
                Result<WorkoutPlan> workoutPlanOrError = new(
                    value: new WorkoutPlan { Name = string.Empty, TrainerId = Guid.Empty, Id = Guid.Empty },
                    isSuccess: false,
                    message: "Workout Plan Not Found");

                return workoutPlanOrError;
            }

            manageFitDbContext.WorkoutPlanExercise.RemoveRange(workoutPlan.WorkoutPlanExercise);
            manageFitDbContext.WorkoutPlanClient.RemoveRange(workoutPlan.WorkoutPlanClient);
            manageFitDbContext.WorkoutPlan.Remove(workoutPlan);

            try
            {
                await manageFitDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Result<WorkoutPlan> workoutPlanOrError = new(
                    value: new WorkoutPlan { Name = string.Empty, TrainerId = Guid.Empty, Id = Guid.Empty },
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
                    value: new WorkoutPlan { Name = string.Empty, TrainerId = Guid.Empty, Id = Guid.Empty },
                    isSuccess: false,
                    message: "Workout Plan Not Found");

                return workoutPlanOrError;
            }

            return new Result<WorkoutPlan>(
                value: workoutPlan,
                isSuccess: true,
                message: "Valid Data");
        }

        public async Task<Result<IEnumerable<WorkoutPlan>>> GetWorkoutPlansByTrainerId(Guid trainerId, CancellationToken cancellationToken)
        {
            IEnumerable<WorkoutPlan> workoutPlans = await manageFitDbContext.WorkoutPlan
                .Include(x => x.WorkoutPlanClient)
                .ThenInclude(x => x.Clients)
                .Include(x => x.WorkoutPlanExercise)
                .ThenInclude(x => x.Exercises)
                .Where(workoutPlan => workoutPlan.TrainerId == trainerId)
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
                    value: new WorkoutPlan { Name = string.Empty, TrainerId = Guid.Empty, Id = Guid.Empty },
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
