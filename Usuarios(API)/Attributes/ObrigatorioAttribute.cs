using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Usuarios_API_.Data.DTOs;
using Usuarios_API_.Exceptions;

namespace Usuarios_API_.Attributes;

public class ObrigatorioAttribute : RequiredAttribute
{
	public ObrigatorioAttribute() : base()
	{
	}

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (!base.IsValid(value))
        {
            var campo = validationContext.MemberName.ToString();
            throw new BadRequestException($" O campo {campo} é obrigatório.");
        }

        return ValidationResult.Success;
    }
}
