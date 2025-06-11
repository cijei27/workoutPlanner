using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WorkoutPlanner.Domain.Entities.Exercise;
using WorkoutPlanner.Domain.Enums;
using WorkoutPlanner.Domain.Interfaces.ExternalServices;
using WorkoutPlanner.Infrastructure.ExternalServices.OpenAI.Models;

namespace WorkoutPlanner.Infrastructure.ExternalServices.OpenAI.Repositories
{
    public class ExerciseOpenAIRepository : IExerciseOpenAIRepository
    {
        private readonly IOpenAIClient _openAIClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ExerciseOpenAIRepository(IOpenAIClient openAIClient)
        {
            _openAIClient = openAIClient ?? throw new ArgumentNullException(nameof(openAIClient));
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }
        public async Task<ExerciseEntity> FeedExerciseAsync(ExerciseEntity exercise, CancellationToken cancellationToken = default)
        {
            // construimos el prompt y llamamos a sendChatCompletion

            var promptCreation = new StringBuilder();

            // Sistema: rol y contexto
            promptCreation.AppendLine("Eres un entrenador personal experto en calistenia y nutrición.");
            promptCreation.AppendLine($"Para el ejercicio \"{exercise.Name}\",");

            // Instrucción y formato de salida
            promptCreation.AppendLine("proporcióname un JSON válido con estas propiedades:");
            promptCreation.AppendLine("  • name: nombre exacto del ejercicio.");
            promptCreation.AppendLine("  • description: explicación detallada de técnica, variaciones y errores comunes.");
            promptCreation.AppendLine("  • videoURL: enlace de YouTube que EXISTA y pertenezca a los canales oficiales de:");
            promptCreation.AppendLine("  • Linn Lowes (channel/@LINNLOWES01)");
            promptCreation.AppendLine("  • Chris Heria (channel/@CHRISHERIA)");
            promptCreation.AppendLine("  • Arnold Schwarzenegger (channel/@ArnoldSchwarzenegger)\n");
            promptCreation.AppendLine($"Si no existen vídeos en esos canales, selecciona el primero que encuentres de calidad en YouTube con más de 10,000 vistas para {exercise.Name}");
            promptCreation.AppendLine("  • exerciseCategory: número según FullBody(0), UpperBody(1), LowerBody(2), Core(3), Cardio(4), Glutes(5), Back(6), Chest(7), Arms(8), Legs(9).");
            promptCreation.AppendLine("  • targetMuscles: lista de músculos implicados.");
            promptCreation.AppendLine();
            promptCreation.AppendLine("Devuelve SOLO el JSON, sin texto adicional.");
            promptCreation.AppendLine("");
            promptCreation.AppendLine();
            promptCreation.AppendLine("Ejemplo de salida esperada:");
            promptCreation.AppendLine("{");
            promptCreation.AppendLine("  \"name\": \"Sentadillas 3\",");
            promptCreation.AppendLine("  \"description\": \"Sentadillas con barra para cuádriceps y glúteos\",");
            promptCreation.AppendLine("  \"videoURL\": \"https://youtu.be/EjemploSentadillas\",");
            promptCreation.AppendLine("  \"exerciseCategory\": 2,");
            promptCreation.AppendLine("  \"targetMuscles\": [\"Quadriceps\",\"Glutes\"]");
            promptCreation.AppendLine("}");

            // 2) Enviar a OpenAI y obtener el JSON como cadena
            string jsonResponse = await _openAIClient.SendChatCompletionAsync(promptCreation.ToString(), cancellationToken);
            // 3) Deserializar el JSON a ExerciseOpenAIResponse
            var exerciseOpenAI = JsonSerializer.Deserialize<ExerciseOpenAIResponse>(jsonResponse, _jsonOptions);

            if (exerciseOpenAI is null)
                throw new InvalidOperationException("OpenAI devolvió un JSON inválido o no pudo deserializarse a ExerciseOpenAIResponse.");

            // 4) Mapear cada campo del DTO al ExerciseEntity recibido
            exercise.Name = exerciseOpenAI.Name;
            exercise.Description = exerciseOpenAI.Description;
            exercise.VideoURL = exerciseOpenAI.VideoURL;
            exercise.TargetMuscles = exerciseOpenAI.TargetMuscles;
            exercise.Category = Enum.IsDefined(typeof(ExerciseCategory), exerciseOpenAI.ExerciseCategory) ? (ExerciseCategory)exerciseOpenAI.ExerciseCategory : ExerciseCategory.FullBody;

            return exercise;


        }

        public async Task<string> RecognizeExerciseAsync(string videoURL, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /*
        * Antiguo prompt
            promptCreation.AppendLine("actua como un experto en entrenamiento personal, calistenia, nutrición y dime a partir ");
            promptCreation.AppendLine("de un nombre de un ejercicio que te pongo, la descripción, una URL de youtube para ver ");
            promptCreation.AppendLine("ese ejercicio(quiero que seas preciso y en el videoURL que los videos los busques de los ");
            promptCreation.AppendLine($"atletas: Linn Lowes, Chris Heria y arnold schwarzenegger. Sé preciso con esto, si no encuentras un video suyo, proporcioname otro video en general de este ejercicio {exercise.Name}.), la categoria ");
            promptCreation.AppendLine("del ejercicio, los musculos implicados. Quiero que tomes atletas como referencia como por ");
            promptCreation.AppendLine("ejemplo Linn Lowes, Chris Heria, Arnold schwarzenegger y me des el resultado en formato json, quiero que seas ");
            promptCreation.AppendLine("preciso, quiero que la respuesta sea como este JSON:");
            promptCreation.AppendLine();
            promptCreation.AppendLine("{");
            promptCreation.AppendLine("  \"name\": \"Sentadillas 3\",");
            promptCreation.AppendLine("  \"description\": \"Sentadillas con barra para cuádriceps y glúteos\",");
            promptCreation.AppendLine("  \"videoURL\": \"https://youtu.be/EjemploSentadillas\",");
            promptCreation.AppendLine("  \"exerciseCategory\": 2,");
            promptCreation.AppendLine("  \"targetMuscles\": [ \"Quadriceps\", \"Glutes\" ]");
            promptCreation.AppendLine("}");
            promptCreation.AppendLine();
            promptCreation.AppendLine("donde las categorias se clasifican en : FullBody(0), UpperBody(1),LowerBody(2),Core(3),Cardio(4),");
            promptCreation.AppendLine("Glutes(5),Back(6),Chest(7),Arms(8),Legs(9).");
            promptCreation.AppendLine();
            promptCreation.AppendLine($"El nombre del ejercicio que quiero que busques es: {exercise.Name}.");
            promptCreation.AppendLine();
            promptCreation.AppendLine("Por favor asegúrate de que todos los valores coincidan exactamente con el formato especificado. ");
            promptCreation.AppendLine("La respuesta debe ser un JSON válido. Devuelve solo JSON válido sin comentarios o explicaciones adicionales. ");
            promptCreation.AppendLine("Si por algún casual no encuentras un video correspondiente de los atletas nombrados, siempre puedes buscar ");
            promptCreation.AppendLine("otro video de atletas similares que si tengan un video con el tipo de ejercicio que te indico anteriormente.");
            promptCreation.AppendLine();
            promptCreation.AppendLine("Además quiero que la descripción del ejercicio sea detallada y que explique detenidamente la técnica ");
            promptCreation.AppendLine("correcta para ejecutarlo de forma segura. Añade variaciones más avanzadas y sencillas si aplica. ");
            promptCreation.AppendLine("También menciona errores comunes que debe evitar el usuario al realizarlo, todo en un lenguaje natural ");
            promptCreation.AppendLine("y que el usuario lo entienda perfectamente, como si se estuviera imaginando el ejercicio.");
            promptCreation.AppendLine();*/
    }
}