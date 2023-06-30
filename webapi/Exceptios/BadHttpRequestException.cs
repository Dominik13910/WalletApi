namespace webapi.Exceptios
{
    public class BadHttpRequestException: Exception

    {
        public BadHttpRequestException(string message): base(message)
        { 
        }
    }
}
