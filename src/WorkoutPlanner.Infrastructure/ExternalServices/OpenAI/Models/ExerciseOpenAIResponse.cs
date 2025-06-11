using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorkoutPlanner.Infrastructure.ExternalServices.OpenAI.Models
{
    internal record ExerciseOpenAIResponse
    {
        [property: JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [property: JsonPropertyName("description")]
        public string Description { get; init; } = string.Empty;

        [property: JsonPropertyName("videoURL")]
        public string VideoURL { get; init; } = string.Empty;

        /// <summary>
        /// Se espera un número entero que coincida con WorkoutPlanner.Domain.Enums.ExerciseCategory.
        /// </summary>
        [property: JsonPropertyName("exerciseCategory")]
        public int ExerciseCategory { get; init; }

        [property: JsonPropertyName("targetMuscles")]
        public List<string> TargetMuscles { get; init; } = new();
    }
}