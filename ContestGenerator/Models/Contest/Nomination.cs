namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Класс предоставляющий информацию о существующих номинациях
    /// </summary>
    public class Nomination
    {
        public int Id { get; set; }

        /// <summary>
        /// Название номинации
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание номинации
        /// </summary>
        public string Description { get; set; }
    }
}
