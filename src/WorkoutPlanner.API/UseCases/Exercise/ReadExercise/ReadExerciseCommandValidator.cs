using FluentValidation;

namespace WorkoutPlanner.API.UseCases.Exercise.ReadExercise
{
    public class ReadExerciseCommandValidator : AbstractValidator<ReadExerciseCommand>
    {
        public ReadExerciseCommandValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("El número de página tiene que ser mayor que cero");

            RuleFor(x => x.Limit)
                .GreaterThan(0)
                .WithMessage("El límite debe ser mayor que cero.")
                .LessThanOrEqualTo(50)
                .WithMessage("El límite no puede exceder 50 registros por página.");
        }
    }
}