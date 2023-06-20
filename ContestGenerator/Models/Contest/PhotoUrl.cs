using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Класс для хранения ссылки на фото
    /// </summary>
    public class PhotoUrl
    {
        [Key]
        public int? Id { get; set; }

        /// <summary>
        /// Ссылка на фото
        /// </summary>
        public string Url { get; set; }
    }
}
