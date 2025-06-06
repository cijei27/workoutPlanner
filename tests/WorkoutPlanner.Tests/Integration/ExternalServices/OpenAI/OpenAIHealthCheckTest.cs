using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WorkoutPlanner.Domain.Interfaces.ExternalServices;
using WorkoutPlanner.Infrastructure.ExternalServices.OpenAI;
using WorkoutPlanner.Infrastructure.ExternalServices.OpenAI.Settings;
using Xunit;
namespace WorkoutPlanner.Tests.Integration.ExternalServices.OpenAI
{
    public class OpenAIHealthCheckTest
    {
        private const string EnvVarName = "OPENAI_API_KEY";
        [Fact(DisplayName = "✔ OpenAI API HealthCheck: responde a un prompt \"ping\"")]
        public async Task OpenAI_ApiIsReachable_ReturnsNonEmptyResponse()
        {
            // 1) Leer la clave de OpenAI de la variable de entorno
            var apiKey = Environment.GetEnvironmentVariable(EnvVarName);
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                // Si no hay clave configurada, omitimos este test en lugar de fallar.
                throw new SkipTestException($"Variable de entorno '{EnvVarName}' no configurada. " + "Se omite HealthCheck de OpenAI.");
            }

            // 2) Construir un OpenAISettings “en memoria”
            var settings = Options.Create(new OpenAISettings
            {
                ApiKey = apiKey,
                BaseURL = "https://api.openai.com/v1/",
                ModelUsed = "gpt-4o-mini",
                TimeoutSeconds = 30,
                TypeText = "plain_text",
                Temperature = 0.0
            });

            // 3) Crear un HttpClient real apuntando a OpenAI
            //    ─ Puedes usar IHttpClientFactory en producción, pero en el test lo instanciamos directo.
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(settings.Value.BaseURL),
                Timeout = TimeSpan.FromSeconds(settings.Value.TimeoutSeconds)
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            // 4) Instanciar el OpenAIService real
            IOpenAIClient openAi = new OpenAIService(httpClient, settings);

            // 5) Enviar un prompt muy breve ("ping") y obtener la respuesta
            string prompt = "ping";
            string response = string.Empty;

            var exceptionThrown = false;
            try
            {
                response = await openAi.SendChatCompletionAsync(prompt);
            }
            catch (HttpRequestException hex)
            {
                exceptionThrown = true;
                // si hay error de red o 401, indicamos que no se pudo conectar
                Assert.False(true, $"HTTP request exception al conectar a OpenAI: {hex.Message}");
            }
            catch (Exception ex)
            {
                exceptionThrown = true;
                Assert.False(true, $"Error inesperado al llamar a OpenAI: {ex.GetType().Name} / {ex.Message}");
            }

            // 6) Si no hubo excepción, aseguramos que la respuesta no sea nula o vacía
            Assert.False(exceptionThrown, "Ocurrió una excepción al llamar a OpenAI.");
            Assert.False(string.IsNullOrWhiteSpace(response), "La respuesta de OpenAI vino vacía.");
        }

        private class SkipTestException : Xunit.Sdk.XunitException
        {
            public SkipTestException(string message) : base($"[SKIPPED] {message}") { }
        }
    }
}