
using System.ComponentModel.DataAnnotations;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.Application.UseCases.Exercise.DeleteExercise;

namespace WorkoutPlanner.API.UseCases.Exercise.DeleteExercise
{
    public record DeleteExerciseCommand(
        Guid IdExercise
        ) : IRequest<ErrorOr<DeleteExerciseOutput>>;


}