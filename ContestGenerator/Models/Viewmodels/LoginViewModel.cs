using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ContestGenerator.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
