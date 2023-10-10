using ContestGenerator.Helpers;
using SignHere.Database;
using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models
{
    public class UploadFileViewmodel
    {
        [Required]
        [Display(Name = "Файл")]
        [AllowedExtensions]
        public IFormFile File { get; set; }

        [Required]
        [Display(Name = "Название файла")]
        public string Name { get; set; }
    }
}
