namespace ContestGenerator.Models.Contest
{

    /// <summary>
    /// Класс содержащий информацию о шагах
    /// для подготовки работы
    /// </summary>
    public class Step
    {
        public int Id { get; set; }

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
