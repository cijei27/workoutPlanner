using WorkoutPlanner.Application.Interfaces;
using WorkoutPlanner.Domain.Entities.Exercise;
using WorkoutPlanner.Domain.Interfaces;
using WorkoutPlanner.Domain.Errors;
using ErrorOr;
using System;
using System.Threading.Tasks;
using WorkoutPlanner.Application.UseCases.Exercise.CreateExercise.Interfaces;

namespace WorkoutPlanner.Application.UseCases.Exercise.CreateExercise
{

    public class CreateExerciseUseCase : ICreateExerciseUseCase
    {
        private readonly IExerciseRepository _repository;

        public CreateExerciseUseCase(IExerciseRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        /// <summary>
        /// Executes the use case
        /// </summary>
        /// <param name="input">Input with all the information about one exercise</param>
        /// <returns>CreateExerciseOutput</returns>
        public async Task<ErrorOr<CreateExerciseOutput>> ExecuteAsync(CreateExerciseInput input)
        {
            if (input is not null)
            {
                var exercise = new ExerciseEntity(input.Name, input.Description, input.VideoURL, input.ExerciseCategory, input.TargetMuscles);
                var responseExercise = await _repository.CreateExerciseAsync(exercise);
                var outputExercise = new CreateExerciseOutput(responseExercise.Id);
                return outputExercise;

            }

            return Errors.Exercise.ExerciseDoesNotExists;
        }

    }

}