using ErrorOr;
using WorkoutPlanner.Application.UseCases.Exercise.ReadExercise.Interfaces;
using WorkoutPlanner.Domain.Errors;
using WorkoutPlanner.Domain.Interfaces;

namespace WorkoutPlanner.Application.UseCases.Exercise.ReadExercise
{
    public class ReadExerciseUseCase : IReadExerciseUseCase
    {
        private readonly IExerciseRepository _repository;

        public ReadExerciseUseCase(IExerciseRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ErrorOr<ReadExerciseOutput>> ExecuteAsync(ReadExerciseInput input)
        {
            if (input is not null)
            {
                var responseExercises = await _repository.ReadAllExercisesync(input.PageNumber, input.LimitQuery);

                var outputExercises = responseExercises
                .Select(x => new ReadExerciseOutput.ExerciseDTO(
                    x.Id,
                    x.Name,
                    x.Description,
                    x.VideoURL,
                    x.Category,
                    x.TargetMuscles
                )).ToList();

                return new ReadExerciseOutput(outputExercises);
            }
            return Errors.Exercise.ExerciseDoesNotExists;
        }
    }
}