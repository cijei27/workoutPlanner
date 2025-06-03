using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlanner.Application.UseCases.Exercise.DeleteExercise
{
    public class DeleteExerciseOutput
    {
        public Guid IdExercise { get; set; }

        public DeleteExerciseOutput(Guid id)
        {
            IdExercise = id;
        }
    }
}