namespace WorkoutPlanner.Application.UseCases.Exercise.ReadExercise
{
    public class ReadExerciseInput
    {
        public int PageNumber { get; set; }
        public int LimitQuery { get; set; }

        public ReadExerciseInput(int pageNumber, int limitQuery)
        {
            PageNumber = pageNumber;
            LimitQuery = limitQuery;
        }
    }
}