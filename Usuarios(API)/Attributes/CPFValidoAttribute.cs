using System.ComponentModel.DataAnnotations;
using Usuarios_API_.Exceptions;

namespace Usuarios_API_.Attributes;

public class CPFValidoAttribute : ValidationAttribute
{
	public CPFValidoAttribute() : base()
	{

	}

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var cpf = (string)value;

        if (cpf.Length != 11)
            throw new BadRequestException("Digite o CPF com 11 dígitos, sem pontos ou hífen.");

        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;

        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();
        tempCpf += digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito += resto.ToString();

        if (!cpf.EndsWith(digito))
            throw new BadRequestException("CPF informado não é válido.");

        return ValidationResult.Success;
    }
}
