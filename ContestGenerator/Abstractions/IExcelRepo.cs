using ContestGenerator.Models.Contest;

namespace ContestGenerator.Abstractions
{
    /// <summary>
    /// Интерфейс для работы с Excel
    /// </summary>
    public interface IExcelRepo
    {
        /// <summary>
        /// Генерирует таблицу с вопросами
        /// </summary>
        /// <param name="questions">Вопросы</param>
        /// <returns>Файл таблицы</returns>
        Task<byte[]> Generate(IEnumerable<Question> questions);

        /// <summary>
        /// Генерирует таблицу с ответами на форму
        /// </summary>
        /// <param name="responses">Ответы</param>
        /// <returns>Файл таблицы</returns>
        Task<byte[]> Generate(IEnumerable<Response> responses);
    }
}
