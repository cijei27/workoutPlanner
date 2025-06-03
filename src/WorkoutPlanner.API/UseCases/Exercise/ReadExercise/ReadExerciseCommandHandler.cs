using ErrorOr;
using MediatR;
using WorkoutPlanner.API.UseCases.Exercise.ReadExercise;
using WorkoutPlanner.Application.UseCases.Exercise.ReadExercise;
using WorkoutPlanner.Application.UseCases.Exercise.ReadExercise.Interfaces;
using WorkoutPlanner.Domain.Errors;

namespace WorkoutPlanner.API.UseCases.Exercise.ReadExercise
{
    public class ReadExerciseCommandHandler : IRequestHandler<ReadExerciseCommand, ErrorOr<ReadExerciseOutput>>
    {
        private readonly IReadExerciseUseCase _readExerciseUseCase;

        public ReadExerciseCommandHandler(IReadExerciseUseCase readExerciseCaseUse)
        {
            _readExerciseUseCase = readExerciseCaseUse ?? throw new ArgumentNullException(nameof(readExerciseCaseUse));
        }

        public async Task<ErrorOr<ReadExerciseOutput>> Handle(ReadExerciseCommand request, CancellationToken cancellationToken)
        {
            if (request is not null)
            {
                var limit = request.Limit;
                var skip = (request.Page - 1) * limit;
                var input = new ReadExerciseInput(skip, limit);
                var response = await _readExerciseUseCase.ExecuteAsync(input);
                return response;
            }

            return Errors.Exercise.ExerciseNotFound;
        }

    }
}
