using WorkoutPlanner.Application.Interfaces;
using WorkoutPlanner.Domain.Entities.Exercise;
using WorkoutPlanner.Domain.Interfaces;
using WorkoutPlanner.Domain.Errors;
using ErrorOr;
using System;
using System.Threading.Tasks;
using WorkoutPlanner.Application.UseCases.Exercise.CreateExercise.Interfaces;
using WorkoutPlanner.Domain.Interfaces.ExternalServices;

namespace WorkoutPlanner.Application.UseCases.Exercise.CreateExercise
{

    public class CreateExerciseUseCase : ICreateExerciseUseCase
    {
        private readonly IExerciseRepository _repository;
        private readonly IExerciseOpenAIRepository _openAIRepository;

        public CreateExerciseUseCase(IExerciseRepository repository, IExerciseOpenAIRepository openAIRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _openAIRepository = openAIRepository ?? throw new ArgumentNullException(nameof(openAIRepository));
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

                ExerciseEntity exerciseToInsert;

                try
                {
                    // Alimentamos el ejercicio con OpenAI
                    exerciseToInsert = await _openAIRepository.FeedExerciseAsync(exercise);
                }
                catch (Exception)
                {
                    //TODO: Aquí haremos un log, para que capturemos el exception y podamos ver en el log lo que ha pasado
                    // Si algo falla al llamar a OpenAI, insertamos el ejercicio original tal cual
                    exerciseToInsert = exercise;
                }

                var responseExercise = await _repository.CreateExerciseAsync(exerciseToInsert);
                var outputExercise = new CreateExerciseOutput(responseExercise.Id);
                return outputExercise;

            }

            return Errors.Exercise.ExerciseDoesNotExists;
        }

    }

}