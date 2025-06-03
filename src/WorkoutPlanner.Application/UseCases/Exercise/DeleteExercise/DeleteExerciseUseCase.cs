using ErrorOr;
using WorkoutPlanner.Application.UseCases.Exercise.DeleteExercise.Interfaces;
using WorkoutPlanner.Domain.Errors;
using WorkoutPlanner.Domain.Interfaces;

namespace WorkoutPlanner.Application.UseCases.Exercise.DeleteExercise
{
    public class DeleteExerciseUseCase : IDeleteExerciseUseCase
    {
        private readonly IExerciseRepository _repository;

        public DeleteExerciseUseCase(IExerciseRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        }
        public async Task<ErrorOr<DeleteExerciseOutput>> ExecuteAsync(DeleteExerciseInput input)
        {
            if (input.IdExercise != Guid.Empty)
            {
                bool exists = await _repository.ExistsExerciseAsync(input.IdExercise);
                if (!exists)
                {
                    return Errors.Exercise.ExerciseNotFound;
                }
                await _repository.DeleteExerciseAsync(input.IdExercise);

                return new DeleteExerciseOutput(input.IdExercise);
            }

            return Errors.Exercise.ExerciseIdNotCorrect;

        }
    }
}