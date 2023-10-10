using SignHere;
using SignHere.Database;
using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Helpers
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var fileStream = file.OpenReadStream();
                if (fileStream.Length != 0)
                {
                    var signature = SignHere.SignHere.FindSignature(fileStream);
                    if (signature is null)
                    {
                        return new ValidationResult("Недопустимый тип файла");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
