using Swashbuckle.AspNetCore.Filters;
using WorkoutPlanner.API.UseCases.CreateExercise;
using WorkoutPlanner.API.UseCases.Exercise.CreateExercise;
using WorkoutPlanner.Domain.Enums;

namespace WorkoutPlanner.API.Common.Doc.Swagger.UseCases.Exercise
{
    /// <summary>
    /// Ejemplo de payload para CreateExerciseCommandExample
    /// </summary>
    public class CreateExerciseCommandExample : IExamplesProvider<CreateExerciseCommand>
    {
        public CreateExerciseCommand GetExamples()
                   => new CreateExerciseCommand(
                          Name: "Sentadillas",
                          Description: "Sentadillas con barra para cuádriceps y glúteos",
                          VideoURL: "https://youtu.be/EjemploSentadillas",
                          ExerciseCategory: ExerciseCategory.LowerBody,
                          TargetMuscles: new List<string> { "Quadriceps", "Glutes" }
                      );
    }
}