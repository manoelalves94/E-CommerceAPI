namespace Usuarios_API_.Exceptions;

public class BadRequestException : Exception
{
	public BadRequestException(string mensagem) : base(mensagem)
	{

	}
}
