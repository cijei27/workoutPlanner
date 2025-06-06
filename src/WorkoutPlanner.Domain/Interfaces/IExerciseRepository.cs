using WorkoutPlanner.Domain.Entities.Exercise;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace WorkoutPlanner.Domain.Interfaces
{
    public interface IExerciseRepository
    {
        Task<ExerciseEntity> ReadExerciseByIdAsync(Guid id);
        Task<List<ExerciseEntity>> ReadAllExercisesync(int skip, int limit);
        Task<ExerciseEntity> CreateExerciseAsync(ExerciseEntity exercise);
        Task<bool> ExistsExerciseAsync(Guid id);
        Task DeleteExerciseAsync(Guid id);
        // Añadir update
    }
}
