using ErrorOr;
namespace WorkoutPlanner.Application.Interfaces
{
    /// <summary>
    /// Contrato genérico de un caso de uso
    /// </summary>
    /// <typeparam name="TRequest">Tipo de objeto de entrada (ej. LoginUserInput).</typeparam>
    /// <typeparam name="TResponse">Tipo de objeto de salida (ej. LoginUserOutput).</typeparam>
    /// <param name="request"></param>
    /// <returns>ErrorOr de TREsponse</returns>
    public interface IUseCase<TRequest, TResponse> where TRequest : class where TResponse : class
    {
        /// <summary>
        /// Ejecuta la lógica del caso de uso con los datos de entrada y devuelve un resultado o errores.
        /// </summary>
        /// <param name="request">Datos de entrada (TRequest).</param>
        /// <returns>ErrorOr de TResponse.</returns>
        Task<ErrorOr<TResponse>> ExecuteAsync(TRequest request);
    }

}