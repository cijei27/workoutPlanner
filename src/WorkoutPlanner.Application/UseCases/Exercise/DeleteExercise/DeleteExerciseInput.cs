using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlanner.Application.UseCases.Exercise.DeleteExercise
{
    public class DeleteExerciseInput
    {
        public Guid IdExercise { get; set; }

        public DeleteExerciseInput(Guid idExercise)
        {
            IdExercise = idExercise;
        }

    }
}