using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace E_Commerce_API_.Attributes;

public class VerificacaoDaPesquisaAttribute : ValidationAttribute
{
    public VerificacaoDaPesquisaAttribute() : base("A pesquisa não pôde ser realizada.")
    {

    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var minCaracteres = 3;
        var maxCaracteres = 128;

        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
        var nome = (string)value;

        if(nome == null)
        {
            return null;
        }

        nome = nome.ToLower();
        nome = ti.ToTitleCase(nome);

        Log.Information("Iniciando verificação do termo pesquisado: '{@nome}'.", nome);

        if (!nome.Replace(" ", "").All(char.IsLetter))
        {
            Log.Error("Erro: O termo pesquisado '{@nome}' não pode conter caracteres especiais.", nome);
            return new ValidationResult("A pesquisa não aceita caracteres especiais.", new string[] { base.FormatErrorMessage(validationContext.MemberName) });
        }

        if (nome.Length < minCaracteres)
        {
            Log.Error("Erro: O termo pesquisado '{@nome}' não pode conter menos de {@minCaracteres} caracteres.", nome, minCaracteres);
            return new ValidationResult($"A pesquisa não pode conter menos de {minCaracteres} caracteres.", new string[] { base.FormatErrorMessage(validationContext.MemberName) });
        }

        if (nome.Length > maxCaracteres)
        {
            Log.Error("Erro: O termo pesquisado '{@nome}' não pode conter mais de {@maxCaracteres} caracteres.", nome, maxCaracteres);
            return new ValidationResult($"A pesquisa não pode conter mais de {maxCaracteres} caracteres.", new string[] { base.FormatErrorMessage(validationContext.MemberName) });
        }

        Log.Information("Termo pesquisado está dentro dos critérios.");
        return ValidationResult.Success;

    }
}
