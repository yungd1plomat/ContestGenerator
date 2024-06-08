using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.Viewmodels
{
    public class AddUserViewmodel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
