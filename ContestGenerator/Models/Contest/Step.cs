using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.Contest
{

    /// <summary>
    /// Класс содержащий информацию о шагах
    /// для подготовки работы
    /// </summary>
    public class Step
    {
        [Key]
        public int? Id { get; set; }

        /// <summary>
        /// Название шага
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание шага
        /// </summary>
        public string Description { get; set; }
    }
}
