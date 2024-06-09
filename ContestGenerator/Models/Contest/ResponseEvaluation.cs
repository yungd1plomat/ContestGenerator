using ContestGenerator.Data;

namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Класс предоставляющий информацию об
    /// общей оценке олимпиады по всем критериям
    /// </summary>
    public class ResponseEvaluation
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Результаты оценок
        /// </summary>
        public IList<CriteriaEvaluation> Results { get; set; }

        /// <summary>
        /// Пользователь оценивший заявку
        /// </summary>
        public AppUser User { get; set; }

        /// <summary>
        /// Заявка которая оценивалась
        /// </summary>
        public Response Response { get; set; }

        /// <summary>
        /// Внешний ключ указывающий на Id заявки
        /// </summary>
        public int ResponseId { get; set; }
    }
}
