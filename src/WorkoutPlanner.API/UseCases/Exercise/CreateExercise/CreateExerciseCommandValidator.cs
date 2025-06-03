using FluentValidation;
using WorkoutPlanner.API.UseCases.Exercise.CreateExercise;

namespace WorkoutPlanner.API.UseCases.CreateExercise
{
    public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre no puede estar vacío.")
            .MaximumLength(100).WithMessage("El nombre no puede superar 100 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción es obligatoria.");

            RuleFor(x => x.VideoURL)
                .NotEmpty().WithMessage("La URL del vídeo es obligatoria.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("La URL del vídeo debe ser válida.");

            RuleFor(x => x.ExerciseCategory)
                .IsInEnum().WithMessage("La categoría seleccionada no es válida.");

            RuleFor(x => x.TargetMuscles)
                .NotEmpty().WithMessage("Debe indicarse al menos un músculo objetivo.");
        }
    }
}