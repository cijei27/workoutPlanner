using WorkoutPlanner.Domain.Enums;
using WorkoutPlanner.Domain.Entities.Exercise;

namespace WorkoutPlanner.Domain.Entities.Workout
{
    public class WorkoutSessionEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid WorkoutPlanId { get; set; }
        public DateTime Date { get; set; }
        public SessionType Type { get; set; }
        public List<ExercisePlanEntity> Exercises { get; set; } = new();
        // public CardioInfo? Cardio { get; set; } // opcional
    }

    /*public class CardioInfo
    {
        public string CardioType { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
    }*/
}