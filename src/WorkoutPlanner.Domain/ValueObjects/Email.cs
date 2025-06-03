using System.Text.RegularExpressions;

namespace WorkoutPlanner.Domain.ValueObjects
{
    public record Email
    {
        public string Address { get; private set; }

        private static readonly Regex EmailRegex =
            new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("El email no puede estar vacío.");

            if (!EmailRegex.IsMatch(address))
                throw new ArgumentException("El formato del email no es válido.");

            Address = address.Trim().ToLower();
        }
    }
}
