namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Класс предоставляющий критерий оценки работы
    /// </summary>
    public class Criteria
    {
        /// <summary>
        /// ID 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название критерия
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Олимпиады к которым относится критерий оценки
        /// </summary>
        public IList<Contest>? Contests { get; set; }
    }
}
