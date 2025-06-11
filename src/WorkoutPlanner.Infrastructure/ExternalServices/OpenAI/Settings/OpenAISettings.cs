using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlanner.Infrastructure.ExternalServices.OpenAI.Settings
{
    public class OpenAISettings
    {
        /// <summary>
        /// La API Key de OpenAI
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// La URL base de la api de OpenAI
        /// </summary>
        public string BaseURL { get; set; }

        /// <summary>
        /// El modelo que va a usar chatgpt para ejecutar el prompt
        /// </summary>
        public string ModelUsed { get; set; }

        /// <summary>
        /// Tiempo de espera en segundos antes de abortar la solicitud HTTP
        /// </summary>
        public int TimeoutSeconds { get; set; } = 30;

        /// <summary>
        /// El rol del sistema para los prompts
        /// </summary>
        public string Role { get; set; } = "user";
        /// <summary>
        /// La temperatura que se usa en las llamadas a chatGPT (0.0 muy preciso, 1.0 muy creativo)
        /// </summary>
        public double Temperature { get; set; } = 0.0;

        /// <summary>
        /// Qué formato de texto pedir, texto, imagen, texto plano etc 
        /// </summary>
        public string TypeText { get; set; }

    }
}