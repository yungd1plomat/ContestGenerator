using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.Contest
{
    public class Question
    {
        [Key]
        public int Id { get; set; } 

        public string Email { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Contest Contest { get; set; }
    }
}
