using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.Domain.Entities.Exercise;
using WorkoutPlanner.Domain.Entities.User;

namespace WorkoutPlanner.Domain.Interfaces.ExternalServices
{
    public interface IWorkoutOpenAIRepository
    {
        Task<string> GenerateWorkoutFromUserAsync(UserEntity userEntity, CancellationToken cancellationToken = default);
        Task<string> GenerateWorkoutFromExercisesAsync(List<ExercisePlanEntity> exercises, CancellationToken cancellationToken = default);
    }
}