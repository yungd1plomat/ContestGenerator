using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Абстрактное представление любого конкурса
    /// </summary>
    public class Contest
    {
        [Key]
        public int? Id { get; set; }

        /// <summary>
        /// Английское название для идентификации
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на лого
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Ссылка на главную фотку на странице
        /// </summary>
        public string MainPhotoUrl { get; set; }

        /// <summary>
        /// Короткое название которое отображается рядом с лого
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Полное название конкурса
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Описание конкурса
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Правила конкурса
        /// </summary>
        public string? Rules { get; set; }

        /// <summary>
        /// История конкурса
        /// </summary>
        public string? History { get; set; }

        /// <summary>
        /// Отзывы о данном конкурсе
        /// </summary>
        public IEnumerable<Review>? Reviews { get; set; }

        /// <summary>
        /// Ссылки на фотки
        /// </summary>
        public IEnumerable<PhotoUrl>? PhotoUrls { get; set; }

        /// <summary>
        /// Номинации
        /// </summary>
        public IEnumerable<Nomination>? Nominations { get; set; }

        /// <summary>
        /// Информация о шагах для подготовки и отправки работы
        /// </summary>
        public IEnumerable<Step>? Steps { get; set; }

        /// <summary>
        /// Необходимые поля для формы
        /// </summary>
        public IEnumerable<FormField> FormFields { get; set; }

        /// <summary>
        /// Информация в помощь участнику
        /// </summary>
        public IEnumerable<Help>? Helps { get; set; }

        /// <summary>
        /// Новости
        /// </summary>
        public IEnumerable<News> News { get; set; }

        /// <summary>
        /// Партнеры конкурса
        /// </summary>
        public IEnumerable<Partner>? Partners { get; set; }
    }
}
