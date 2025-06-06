using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlanner.Domain.Interfaces.ExternalServices
{
    /// <summary>
    /// Cliente genérico para enviar prompts a la API de OpenAI (chat/completions).
    /// </summary>
    public interface IOpenAIClient
    {
        /// <summary>
        /// Envía un prompt al endpoint /chat/completions y devuelve el contenido textual de la respuesta.
        /// </summary>
        Task<string> SendChatCompletionAsync(string prompt, CancellationToken cancellationToken = default);
    }
}