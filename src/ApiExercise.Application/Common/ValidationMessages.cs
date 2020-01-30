namespace ApiExercise.Application.Common
{
    public static class ValidationMessages
    {
        public static string GetRequired(string field) => $"{field} is required.";
        public static string GetRequiredLength(string field, int length) => $"{field} required {length} characters.";
        public static string GetAtLeastOneSelected(string field) => $"One {field} at least must to be selected.";
        public static string GetGreaterThan(string field, int length) => $"{field} must be greater than {length}.";
        public static string GetGreaterThan(string field, decimal length) => $"{field} must be greater than {length}.";
        public static string GetTooLong(string field) => $"{field} is too long.";
        public static string GetTooShort(string field) => $"{field} is too short.";
        public static string GetValidRequired(string field) => $"A valid {field} is required.";
        public static string GetItsInUse(string field) => $"{field} is already in use";
        public static string GetOutOfRange(string field) => $"{field} is out of range";
        public static string GetItDoesntExists(string field) => $"{field} doesn't exists";
    }
}
