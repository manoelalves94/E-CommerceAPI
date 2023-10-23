using E_Commerce_API_.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Attributes;

public class TamanhoMinimoAttribute : MinLengthAttribute
{
    public TamanhoMinimoAttribute(int length) : base(length)
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (!base.IsValid(value))
        {
            var campo = validationContext.MemberName.ToString();
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

