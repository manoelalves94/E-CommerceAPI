using System.ComponentModel.DataAnnotations;
using Usuarios_API_.Exceptions;

namespace Usuarios_API_.Attributes;

public class CompararAttribute : CompareAttribute 
{
	public CompararAttribute(string otherProperty) : base(otherProperty)
	{

	}

    public override bool IsValid(object? value)
    {
        if (base.IsValid(value))
            return true;

        throw new BadRequestException(ErrorMessage);
    }
}
