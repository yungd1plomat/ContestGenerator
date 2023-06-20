using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Содержит информацию о поле формы
    /// </summary>
    public class FormField
    {
        [Key]
        public int? Id { get; set; }

        /// <summary>
        /// Название поля
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Предустановленные значения для поля
        /// Выведет Select с указанными опциями
        /// </summary>
        public IEnumerable<Predefined>? Predefined { get; set; }
    }
}
