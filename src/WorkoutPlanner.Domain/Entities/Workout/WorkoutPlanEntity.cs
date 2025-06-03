using System;
using System.Collections.Generic;

namespace WorkoutPlanner.Domain.Entities.Workout
{
    public class WorkoutPlanEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int DaysPerWeek { get; set; }

        public List<WorkoutSessionEntity> Sessions { get; set; } = new List<WorkoutSessionEntity>();

    }
}