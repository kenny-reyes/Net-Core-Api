namespace ApiExercise.Application.Shared
{
    public class ValidationMessages
    {
        public static string GetRequired(string field) => $"{field} is required.";
        public static string GetRequiredLenght(string field, int lenght) => $"{field} required {lenght} characters.";
        public static string GetAtLeastOneSelected(string field) => $"One {field} at least must to be selected.";
        public static string GetGreaterThan(string field, int lenght) => $"{field} must be greater than {lenght}.";
        public static string GetGreaterThan(string field, decimal lenght) => $"{field} must be greater than {lenght}.";
        public static string GetTooLong(string field) => $"{field} is too long.";
        public static string GetTooShort(string field) => $"{field} is too short.";
        public static string GetValidRequired(string field) => $"A valid {field} is required.";
        public static string GetItsInUse(string field) => $"{field} is already in use";
        public static string GetOutOfRange(string field) => $"{field} is out of range";
    }
}
