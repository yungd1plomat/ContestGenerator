
using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.Domain
{
    /// <summary>
    /// Класс предоставляющий информацию о домене
    /// </summary>
    public class Domain
    {
        [Key]
        public int? Id { get; set; }

        public string DomainName { get; set; }

        public Contest.Contest Contest { get; set; }
    }
}
