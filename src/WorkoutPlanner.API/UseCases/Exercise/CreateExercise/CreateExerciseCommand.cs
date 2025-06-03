using ErrorOr;
using MediatR;
using WorkoutPlanner.Application.UseCases.Exercise.CreateExercise;
using WorkoutPlanner.Domain.Enums;
using System.Collections.Generic;

namespace WorkoutPlanner.API.UseCases.Exercise.CreateExercise
{
    /// <summary>
    /// Command para crear un nuevo ejercicio
    /// </summary>
    /// <param name="Name">Nombre del ejercicio</param>
    /// <param name="Description">Descripción detallada de lo que hace el ejercicio</param>
    /// <param name="VideoURL">URL de internet o de alguna plataforma dónde esté alojado el video</param>
    /// <param name="ExerciseCategory">  Las categorías que se pueden almacenar se clasifican en : FullBody(0), UpperBody(1),LowerBody(2),Core(3),Cardio(4),Glutes(5),Back(6),Chest(7),Arms(8),Legs(9)</param>
    /// <param name="TargetMuscles">Músculos implicados al hacer el ejercicio</param>
    /// <remarks>
    /// Ejemplo JSON:
    ///     {
    ///       "name": "Sentadillas",
    ///       "description": "Sentadillas con barra para cuádriceps y glúteos",
    ///       "videoUrl": "https://…",
    ///       "category": 9,
    ///       "targetMuscles": ["Quadriceps","Glutes"]
    ///     }
    /// 
    /// </remarks>
    public record CreateExerciseCommand(
        string Name,
        string Description,
        string VideoURL,
        ExerciseCategory ExerciseCategory,
        List<string> TargetMuscles
    ) : IRequest<ErrorOr<CreateExerciseOutput>>;
}