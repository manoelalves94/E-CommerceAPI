using E_Commerce_API_.Exceptions;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace E_Commerce_API_.Attributes;

public class VerificacaoDoNomeAttribute : ValidationAttribute
{
    public VerificacaoDoNomeAttribute() : base("O nome está fora dos critérios de aceite.")
    {
        
    }

    public override bool IsValid(object? value)
    {
        return base.IsValid(value);
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var minCaracteres = 3;
        var maxCaracteres = 128;

        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
        var nome = (string)value;
        nome = nome.ToLower();
        nome = ti.ToTitleCase(nome);

        Log.Information("Iniciando verificação do nome: '{@nome}'.", nome);

        if (!nome.Replace(" ", "").All(char.IsLetter))
        {
            Log.Error("Erro: O nome '{@nome}' não pode conter caracteres especiais.", nome);
            throw new BadRequestException("O nome não aceita caracteres especiais.");
        }

        if (nome.Length < minCaracteres && nome.Replace(" ", "").Length != 0)
        {
            Log.Error("Erro: O nome '{@nome}' contém menos de {@minCaracteres} caracteres.", nome, minCaracteres);
            throw new BadRequestException($"O nome não pode conter menos de {minCaracteres} caracteres.");
        }

        if (nome.Length > maxCaracteres)
        {
            Log.Error("Erro: O nome '{@nome}' contém mais de {@maxCaracteres} caracteres.", nome, maxCaracteres);
            throw new BadRequestException($"O nome não pode conter mais de {maxCaracteres} caracteres.");
        }

        Log.Information("O nome está dentro dos critérios.");
        return ValidationResult.Success;

    }
}
