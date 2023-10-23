using E_Commerce_API_.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Attributes;

public class ObrigatorioAttribute : RequiredAttribute
{
    public ObrigatorioAttribute() : base()
    {

    }

    public override bool IsValid(object? value)
    {
        if (!base.IsValid(value))
            throw new BadRequestException("O campo é obrigatório.");

        return true;
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
