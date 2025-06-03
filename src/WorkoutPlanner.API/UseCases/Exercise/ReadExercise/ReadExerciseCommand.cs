using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WorkoutPlanner.Application.UseCases.Exercise.ReadExercise;

namespace WorkoutPlanner.API.UseCases.Exercise.ReadExercise
{
    /// <summary>
    /// Command para leer ejercicios con paginación
    /// </summary>
    /// <param name="Page">Número de páginas.</param>
    /// <param name="Limit">Cantidad máxima de registros por página.</param>
    public record ReadExerciseCommand(
        [property: FromQuery(Name = "page")]
        int Page = 1,
        [property: FromQuery(Name = "limit")]
        int Limit = 20
    ) : IRequest<ErrorOr<ReadExerciseOutput>>;

}