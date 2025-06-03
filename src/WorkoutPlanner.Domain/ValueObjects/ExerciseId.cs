using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlanner.Domain.Interfaces;

namespace WorkoutPlanner.Domain.ValueObjects
{

    public record ExerciseId
    {

        //Hago esto porque si le paso el Guid directamente en la clase, no tendría manera de validar si existe ese GUID o no
        public Guid Value { get; init; }

        public ExerciseId(Guid value)
        {
            Value = value;
            if (Value == Guid.Empty)
            {
                throw new ArgumentException("El Guid está vacio");
            }

        }
        public override string ToString() => Value.ToString();
    }
}