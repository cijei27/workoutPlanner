using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WorkoutPlanner.Domain.Interfaces.ExternalServices;
using WorkoutPlanner.Infrastructure.ExternalServices.OpenAI.Settings;

namespace WorkoutPlanner.Infrastructure.ExternalServices.OpenAI
{
    public class OpenAIService : IOpenAIClient
    {
        // La idea aqui es que se utilice un unico HttpClient
        private readonly HttpClient _http;
        private readonly OpenAISettings _settings;
        private readonly JsonSerializerOptions _jsonOptions;

        public OpenAIService(HttpClient http, IOptions<OpenAISettings> optionsAccessor)
        {
            _http = http;
            _settings = optionsAccessor.Value;

            // Configuración JSON para parsear respuestas
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }
        public async Task<string> SendChatCompletionAsync(string prompt, CancellationToken cancellationToken = default)
        {
            // 1) Construir payload para /chat/completions
            var requestBody = new
            {
                model = _settings.ModelUsed,
                messages = new[]
                {
                    new { role = "system",  content = $"Actúa como un experto en entrenamiento personal, calistenia, nutrición. Responde en {_settings.TypeText}." },
                    new { role = "user",    content = prompt }
                },
                temperature = _settings.Temperature
            };

            // 2) Serializar y enviar POST
            var content = JsonContent.Create(requestBody, options: _jsonOptions);
            using var response = await _http.PostAsync("chat/completions", content, cancellationToken: cancellationToken);

            response.EnsureSuccessStatusCode();

            // 3) Leer stream y parsear JSON
            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var document = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

            // 4) Extraer choices[0].message.content
            if (document.RootElement.TryGetProperty("choices", out var choices) && choices.ValueKind == JsonValueKind.Array && choices.GetArrayLength() > 0)
            {
                var firstChoice = choices.EnumerateArray().First();
                if (firstChoice.TryGetProperty("message", out var msgElem) && msgElem.TryGetProperty("content", out var contentElem))
                {
                    return contentElem.GetString()!.Trim();
                }
            }

            throw new InvalidOperationException("OpenAI: respuesta inesperada. No se encontró choices[0].message.content");
        }
    }
}