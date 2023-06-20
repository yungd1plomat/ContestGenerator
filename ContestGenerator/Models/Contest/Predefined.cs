using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Класс для хранения предустановленных значений
    /// </summary>
    public class Predefined
    {
        [Key]
        public int? Id { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
    }
}
