using System.ComponentModel.DataAnnotations;
using Usuarios_API_.Exceptions;

namespace Usuarios_API_.Attributes;

public class VerificacaoDataNascimentoAttribute : ValidationAttribute
{
	public VerificacaoDataNascimentoAttribute() : base("Falha ao cadastrar.")
	{

	}

    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var nascimento = (DateTime)value;

        if (nascimento == DateTime.Today)
            throw new BadRequestException("Data de Nascimento não pode ser igual a data atual.");

        if (nascimento > DateTime.Today)
            throw new BadRequestException("Data de Nascimento não pode ser maior que a data atual.");

        return ValidationResult.Success;
    }
}
