namespace WorkoutPlanner.Domain.ValueObjects
{
    public record UserPreferences
    {
        public int DaysPerWeek { get; private set; }
        public int CardioDays { get; private set; }

        public UserPreferences(int daysPerWeek, int cardioDays)
        {
            if (daysPerWeek < 1 || daysPerWeek > 7)
                throw new ArgumentException("Los días de entrenamiento deben estar entre 1 y 7.");

            if (cardioDays < 0 || cardioDays > daysPerWeek)
                throw new ArgumentException("Los días de cardio no pueden ser mayores que los días de entrenamiento.");

            DaysPerWeek = daysPerWeek;
            CardioDays = cardioDays;
        }
        public override string ToString()
        {
            return $"{DaysPerWeek} días/semana, {CardioDays} con cardio";
        }
    }
}
