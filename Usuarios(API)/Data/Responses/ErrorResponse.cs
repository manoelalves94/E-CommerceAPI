namespace Usuarios_API_.Data.Responses;

public class ErrorResponse
{
    public ErrorResponse(string logref, List<string> messages)
    {
        TraceId = Guid.NewGuid();
        Data = DateTime.Today.ToString("dd/MM/yyyy");
        AddErrors(logref, messages);
    }

    private void AddErrors(string logref, List<string> errorMessages)
    {
        Errors = new ErrorDetails(logref, errorMessages);
    }

    public Guid TraceId { get; private set; }
    public ErrorDetails Errors { get; private set; }
    public string Data { get; private set; }

    public class ErrorDetails
    {
        public ErrorDetails(string logref, List<string> message)
        {
            Logref = logref;
            Messages = message;
        }

        public string Logref { get; private set; }
        public List<string> Messages { get; private set; }

        public void InternalError()
        {
            Messages = new List<string>() { "Ocorreu um erro interno." };
        }
    }


}
