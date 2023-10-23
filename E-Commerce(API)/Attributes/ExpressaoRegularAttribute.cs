using E_Commerce_API_.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Attributes;

public class ExpressaoRegularAttribute : RegularExpressionAttribute
{
	public ExpressaoRegularAttribute(string pattern) : base(pattern)
	{
	}

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (!base.IsValid(value))
        {
            var campo = validationContext.MemberName;
            throw new BadRequestException($"O campo {campo} contém caracteres não permitidos.");
        }

        return ValidationResult.Success;
    }
}
