using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.Domain.Entities.Exercise;

namespace WorkoutPlanner.Domain.Interfaces.ExternalServices
{
    public interface IExerciseOpenAIRepository
    {
        Task<ExerciseEntity> FeedExerciseAsync(ExerciseEntity exercise, CancellationToken cancellationToken = default);
        Task<string> RecognizeExerciseAsync(string videoURL, CancellationToken cancellationToken = default);

    }
}