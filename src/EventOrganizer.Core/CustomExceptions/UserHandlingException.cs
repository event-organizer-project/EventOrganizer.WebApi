namespace EventOrganizer.Core.CustomExceptions
{
    public class UserHandlingException : Exception
    {
        public UserHandlingException()
        { }

        public UserHandlingException(string message) :
            base(message)
        { }

        public UserHandlingException(string message, Exception inner) :
            base(message, inner)
        { }
    }
}
