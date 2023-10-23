using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Usuarios_API_.Exceptions;
using Usuarios_API_.Models;

namespace Usuarios_API_.Attributes;

public class RolesExistentesAttribute : ValidationAttribute
{

    public RolesExistentesAttribute() : base()
    {
    }

    public override bool IsValid(object? value)
    {
        var role = (string)value;
        role = role.ToLower();

        if (role == "admin" || role == "lojista" || role == "cliente")
            return true;

        throw new BadRequestException($"O tipo de usuário {role} não existe.");
    }
}
