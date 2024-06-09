using ContestGenerator.Data;
using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Класс содержащий все ответы
    /// пользователя
    /// </summary>
    public class Response
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Все ответы пользователя
        /// </summary>
        public IList<FormResponse> Responses { get; set; }

        /// <summary>
        /// Конкурс на котором он отвечал
        /// </summary>
        public Contest Contest { get; set; }

        /// <summary>
        /// Пользователь оставивший ответ
        /// </summary>
        public AppUser User { get; set; }

        /// <summary>
        /// Содержит оценки заявки
        /// </summary>
        public List<ResponseEvaluation> ResponseEvaluations { get; set; }
    }
}
