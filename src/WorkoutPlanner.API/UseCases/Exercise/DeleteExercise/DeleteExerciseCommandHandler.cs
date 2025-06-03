using ErrorOr;
using MediatR;
using WorkoutPlanner.Application.UseCases.Exercise.DeleteExercise;
using WorkoutPlanner.Application.UseCases.Exercise.DeleteExercise.Interfaces;
using WorkoutPlanner.Domain.Errors;
namespace WorkoutPlanner.API.UseCases.Exercise.DeleteExercise
{
    public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, ErrorOr<DeleteExerciseOutput>>
    {
        private readonly IDeleteExerciseUseCase _deleteExerciseUseCase;

        public DeleteExerciseCommandHandler(IDeleteExerciseUseCase deleteExerciseCaseUse)
        {
            _deleteExerciseUseCase = deleteExerciseCaseUse ?? throw new ArgumentNullException(nameof(deleteExerciseCaseUse));
        }

        public async Task<ErrorOr<DeleteExerciseOutput>> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            if (request is not null)
            {
                var input = new DeleteExerciseInput(request.IdExercise);
                var response = await _deleteExerciseUseCase.ExecuteAsync(input);
                return response;
            }

            return Errors.Exercise.ExerciseIdNotCorrect;
        }
    }
}