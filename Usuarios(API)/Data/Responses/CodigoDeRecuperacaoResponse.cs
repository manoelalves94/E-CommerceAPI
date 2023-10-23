namespace Usuarios_API_.Data.Responses;

public class CodigoDeRecuperacaoResponse
{
	public CodigoDeRecuperacaoResponse(string codigoDeRecuperacao)
	{
		CodigoDeRecuperação = codigoDeRecuperacao;
	}
    public string CodigoDeRecuperação { get; private set; }
}
