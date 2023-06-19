namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Информация о партнерах конкурса
    /// </summary>
    public class Partner
    {
        public int Id { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на сайт организации
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Ссылка на лого организации
        /// </summary>
        public string PhotoUrl { get; set; }
    }
}
