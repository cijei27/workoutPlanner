using WorkoutPlanner.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace WorkoutPlanner.Domain.Entities.User
{
    public class UserEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; }

        public Email email { get; set; }

        public string Password { get; set; }

        public UserPreferences Preferences { get; set; }
        public List<Guid> WorkoutPlanIds { get; set; }
    }
}