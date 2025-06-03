using WorkoutPlanner.Domain.Enums;
using System.Collections.Generic;

namespace WorkoutPlanner.Application.UseCases.Exercise.CreateExercise
{
    public class CreateExerciseInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoURL { get; set; }
        public ExerciseCategory ExerciseCategory { get; set; }
        public List<string> TargetMuscles { get; set; }

        public CreateExerciseInput(string name, string description, string videoURL, ExerciseCategory exerciseCategory, List<string> targetMuscles)
        {
            Name = name;
            Description = description;
            VideoURL = videoURL;
            ExerciseCategory = exerciseCategory;
            TargetMuscles = targetMuscles;
        }

    }
}