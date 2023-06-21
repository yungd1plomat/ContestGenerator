namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Класс содержащий информацию
    /// об ответе пользователя
    /// </summary>
    public class FormResponse
    {
        public int Id { get; set; }

        /// <summary>
        /// Название поля
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ответ пользователя
        /// </summary>
        public string Value { get; set; }
    }
}
