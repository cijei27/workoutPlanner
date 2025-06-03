using WorkoutPlanner.Domain.Enums;
namespace WorkoutPlanner.Application.UseCases.Exercise.ReadExercise
{
    public class ReadExerciseOutput
    {

        public IReadOnlyList<ExerciseDTO> Exercises { get; init; } = Array.Empty<ExerciseDTO>();

        public record ExerciseDTO
        (
            Guid Id,
            string Name,
            string Description,
            string VideoUrl,
            ExerciseCategory Category,
            IReadOnlyList<string> TargetMuscles
        );

        public ReadExerciseOutput(IReadOnlyList<ExerciseDTO> exercises)
        {
            Exercises = exercises;
        }

    }


}