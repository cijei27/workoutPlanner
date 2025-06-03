using System;

namespace WorkoutPlanner.Application.UseCases.Exercise.CreateExercise
{
    public class CreateExerciseOutput
    {
        public Guid Id { get; set; }

        public CreateExerciseOutput(Guid id)
        {
            Id = id;
        }
        public CreateExerciseOutput()
        {

        }
    }
}