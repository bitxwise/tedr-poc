namespace StudyValidationApi.ValidationRules
{
    public class ValidationException
    {
        public readonly string Message;

        public ValidationException(string message)
        {
            Message = message;
        }
    }
}