namespace Timetablebot.Exceptions
{
    public class EmptyDatabaseTableException : Exception
    { 
        public EmptyDatabaseTableException(string message) : base(message) { }
    }
}
