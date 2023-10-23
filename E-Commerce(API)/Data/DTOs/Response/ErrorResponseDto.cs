namespace E_Commerce_API_.Data.DTOs.Error;

public class ErrorResponseDto
{
    public ErrorResponseDto(string logref, string message)
    {
        TraceId = Guid.NewGuid();
        Data = DateTime.Today.ToString("dd/MM/yyyy");
        Error = new ErrorDetailsDto(logref, message);
    }

    public Guid TraceId { get; private set; }
    public ErrorDetailsDto Error { get; private set; }
    public string Data { get; private set; }

    public class ErrorDetailsDto
    {
        public ErrorDetailsDto(string logref, string message)
        {
            Logref = logref;
            Message = message;
        }

        public string Logref { get; private set; }
        public string Message { get; set; }
    }

    public void GenericError()
    {
        Error.Message = "Ocorreu um erro interno.";
    }
}
