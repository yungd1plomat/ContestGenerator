using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models
{
    public class AddDomainViewmodel
    {
        [Required]
        public string Domain { get; set; }

        [Required]
        public string Contest { get; set; }
    }

}
