using System.ComponentModel.DataAnnotations;
namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Класс содержащий информацию об отзывах
    /// </summary>
    public class Review
    {
        [Key]
        public int? Id { get; set; }

        /// <summary>
        /// Ссылка на аватар пользователя
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Текст отзыва
        /// </summary>
        public string Description { get; set; }

    }
}
