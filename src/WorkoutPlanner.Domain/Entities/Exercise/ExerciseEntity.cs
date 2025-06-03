using System.Collections.Generic;
using WorkoutPlanner.Domain.Enums;
using System;

namespace WorkoutPlanner.Domain.Entities.Exercise
{
    public class ExerciseEntity
    {
        /// <summary>
        /// Se inicializa automaticamente al crear la instancia
        /// </summary>
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoURL { get; set; }
        public ExerciseCategory Category { get; set; }
        public List<string> TargetMuscles { get; set; }

        public ExerciseEntity(string name, string description, string videoURL, ExerciseCategory category, List<string> targetMuscles)
        {
            Name = name;
            Description = description;
            VideoURL = videoURL;
            Category = category;
            TargetMuscles = targetMuscles;

        }
    }
}