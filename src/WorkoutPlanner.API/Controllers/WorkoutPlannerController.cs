using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using WorkoutPlanner.API.Common.Doc.Swagger.UseCases.Exercise;
using WorkoutPlanner.API.UseCases.Exercise.CreateExercise;
using WorkoutPlanner.API.UseCases.Exercise.DeleteExercise;
using WorkoutPlanner.API.UseCases.Exercise.ReadExercise;
using WorkoutPlanner.Application.UseCases.Exercise.CreateExercise;
using WorkoutPlanner.Application.UseCases.Exercise.DeleteExercise;
using WorkoutPlanner.Application.UseCases.Exercise.ReadExercise;


namespace WorkoutPlanner.API.Controllers
{
    /// <summary>
    /// Operaciones sobre ejercicios de entrenamiento
    /// </summary>
    [Route("WorkoutPlanner")]
    public class WorkoutPlannerController : ApiController
    {
        private readonly ISender _mediator;

        public WorkoutPlannerController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea un nuevo ejercicio de entrenamiento
        /// </summary>
        /// <param name="command">Payload con los datos del ejercicio</param>
        /// <returns>El Id del ejercicio creado</returns>
        [HttpPost]
        [Route("v1/Exercise")]
        [SwaggerOperation(
        Summary = "🏋🏻‍♀️ Dar de alta un nuevo ejercicio",
        Description = "Registra un nuevo ejercicio clasificado en categoría, enlace del vídeo y músculos objetivo.  Las categorías que se pueden utilizar se clasifican en : FullBody(0), UpperBody(1),LowerBody(2),Core(3),Cardio(4),Glutes(5),Back(6),Chest(7),Arms(8),Legs(9)",
        Tags = new[] { "Exercises 🎽" }
        )]
        [ProducesResponseType(typeof(CreateExerciseOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerRequestExample(typeof(CreateExerciseCommand), typeof(CreateExerciseCommandExample))]
        public async Task<IActionResult> CreateExercise([FromBody] CreateExerciseCommand command)
        {
            var createResult = await _mediator.Send(command);

            return createResult.Match(
                exercise => Ok(exercise),
                errors => Problem(errors));
        }

        [HttpGet]
        [Route("v1/Exercise")]
        [SwaggerOperation(
        Summary = "🧾🏋🏻‍♀️ Consultar todos los ejercicios",
        Description = "Devuelve todos los ejercicios almacenados en el sistema",
        Tags = new[] { "Exercises 🎽" }
        )]
        [ProducesResponseType(typeof(ReadExerciseOutput.ExerciseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadExercise([FromQuery] ReadExerciseCommand command)
        {
            var readResult = await _mediator.Send(command);

            return readResult.Match(
                exercise => Ok(exercise.Exercises),
                errors => Problem(errors));
        }

        [HttpDelete("v1/Exercise/{IdExercise:guid}")]
        [SwaggerOperation(
        Summary = " ❌🏋🏻‍♀️ Borrar un ejercicio",
        Description = "Borra un ejercicio del sistema",
        Tags = new[] { "Exercises 🎽" }
        )]
        [ProducesResponseType(typeof(DeleteExerciseOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteExercise([FromRoute] DeleteExerciseCommand command)
        {
            var deleteResult = await _mediator.Send(command);

            return deleteResult.Match(
                exercise => Ok(exercise),
                errors => Problem(errors));
        }
    }
}