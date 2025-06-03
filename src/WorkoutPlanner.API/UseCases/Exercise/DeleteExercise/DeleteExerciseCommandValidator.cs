using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WorkoutPlanner.API.UseCases.Exercise.DeleteExercise
{
    public class DeleteExerciseCommandValidator : AbstractValidator<DeleteExerciseCommand>
    {
        public DeleteExerciseCommandValidator()
        {
            RuleFor(x => x.IdExercise)
                .NotEmpty().WithMessage("El Id del ejercicio no puede estar vacío, se necesita para borrarlo");
        }

    }
}