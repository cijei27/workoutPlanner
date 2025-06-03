using ErrorOr;

namespace WorkoutPlanner.Domain.Errors
{
    public static partial class Errors
    {
        public static class Workout
        {
            public static Error WorkoutDoesNotExists => Error.Validation("Workout.IdExercise", "Workout does not exists in our database");
            public static Error WorkoutNotCreated => Error.Validation("Workout.IdExercise", "Workout could not created");
        }
    }
}