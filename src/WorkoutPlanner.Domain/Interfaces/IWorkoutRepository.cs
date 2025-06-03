using WorkoutPlanner.Domain.Entities.Workout;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WorkoutPlanner.Domain.Interfaces
{
    public interface IWorkoutRepository
    {
        Task<WorkoutPlanEntity> ReadPlanByIdAsync(Guid planId);
        Task<List<WorkoutPlanEntity>> ReadPlansByUserIdAsync(Guid userId);
        Task<WorkoutPlanEntity> AddPlanAsync(WorkoutPlanEntity plan);

        //añadir delete and update
    }
}
