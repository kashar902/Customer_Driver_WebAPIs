using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BL.Validations;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;
    public AllowedExtensionsAttribute(string[] extensions )
    {
        _extensions = extensions;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(
                    $"This {extension} extension is not allowed! Allowed extensions are '.jpg', '.png' ,'.jpe'");
            }
        }

        return ValidationResult.Success;
    }
}