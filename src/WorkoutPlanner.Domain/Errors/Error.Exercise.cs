using ErrorOr;

namespace WorkoutPlanner.Domain.Errors
{
    public static partial class Errors
    {
        public static class Exercise
        {
            public static Error ExerciseDoesNotExists => Error.Validation("Exercise.IdExercise", "Exercise does not exists in our database");
            public static Error ExerciseNotCreated => Error.Validation("Exercise.IdExercise", "Exercise could not created");
            public static Error ExerciseNotFound => Error.Validation("Exercise.IdExercise", "Exercise not found or maybe there are not exercise. Try to add new ones.");
            public static Error ExerciseIdNotCorrect => Error.Validation("Exercise.IdInvalid", "El Id del ejercicio no puede ser Guid.Empty.");
        }
    }
}