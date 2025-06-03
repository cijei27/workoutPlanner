using System;
using WorkoutPlanner.Domain.ValueObjects;

namespace WorkoutPlanner.Domain.Entities.Exercise
{
    public class ExercisePlanEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public ExerciseId ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int? DurationMinutes { get; set; } // Solo para cardio

        public ExercisePlanEntity(ExerciseId exerciseId, int sets, int reps, int? durationMinutes)
        {
            ExerciseId = exerciseId;
            Sets = sets;
            Reps = reps;
            DurationMinutes = durationMinutes;
        }
    }
}