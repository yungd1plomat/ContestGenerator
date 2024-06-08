using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.Viewmodels
{
    public class AddCriteriaViewmodel
    {
        [Required]
        public string Name { get; set; }
    }
}
