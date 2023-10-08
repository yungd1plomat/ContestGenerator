using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Класс содержащий информацию
    /// о разделе новости
    /// </summary>
    public class News
    {
        [Key]
        public int? Id { get; set; }

        /// <summary>
        /// Заголовок новости
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Краткое описание новости
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ссылка на новость
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Ссылка на фото для новости
        /// </summary>
        public string PhotoLink { get; set; }
    }
}
