using System.ComponentModel.DataAnnotations;
using Usuarios_API_.Exceptions;

namespace Usuarios_API_.Attributes;

public class TamanhoMinimoAttribute : MinLengthAttribute
{
    public TamanhoMinimoAttribute(int length) : base(length)
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (!base.IsValid(value))
        {
            var campo = validationContext.MemberName;
            throw new BadRequestException($" O campo {campo} deve possuir um tamanho mínimo de {Length} caracteres.");
        }

        return ValidationResult.Success;
    }
}

public class TamanhoMaximoAttribute : MaxLengthAttribute
{
    public TamanhoMaximoAttribute(int length) : base(length)
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (!base.IsValid(value))
        {
            var campo = validationContext.MemberName.ToString();
            throw new BadRequestException($" O campo {campo} deve possuir um tamanho máximo de {Length} caracteres.");
        }

        return ValidationResult.Success;
    }
}

