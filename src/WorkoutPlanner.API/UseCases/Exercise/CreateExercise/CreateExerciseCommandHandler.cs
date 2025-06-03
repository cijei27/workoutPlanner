using ErrorOr;
using MediatR;
using WorkoutPlanner.API.UseCases.Exercise.CreateExercise;
using WorkoutPlanner.Application.UseCases.Exercise.CreateExercise;
using WorkoutPlanner.Application.UseCases.Exercise.CreateExercise.Interfaces;
using WorkoutPlanner.Domain.Errors;
namespace WorkoutPlanner.API.UseCases.CreateExercise
{
    public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, ErrorOr<CreateExerciseOutput>>
    {
        private readonly ICreateExerciseUseCase _createExerciseUseCase;

        public CreateExerciseCommandHandler(ICreateExerciseUseCase createExerciseUseCase)
        {
            _createExerciseUseCase = createExerciseUseCase ?? throw new ArgumentNullException(nameof(createExerciseUseCase));
        }

        public async Task<ErrorOr<CreateExerciseOutput>> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            if (request is not null)
            {
                var input = new CreateExerciseInput(request.Name, request.Description, request.VideoURL, request.ExerciseCategory, request.TargetMuscles);
                var response = await _createExerciseUseCase.ExecuteAsync(input);
                return response;
            }

            return Errors.Exercise.ExerciseNotCreated;
        }
    }
}