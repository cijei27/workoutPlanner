using ErrorOr;

namespace WorkoutPlanner.Domain.Errors
{
    public static partial class Errors
    {
        public static class ExternalService
        {
            public static Error ExternalServiceError => Error.Failure("ExternalService.Error", "Error with the connection");

        }
    }
}