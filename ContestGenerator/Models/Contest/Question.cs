using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Класс соджержащий информацию о вопросах по олимпиаде
    /// </summary>
    public class Question
    {
        [Key]
        public int Id { get; set; } 

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Вопрос
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Олимпиада к которой этот вопрос относится
        /// </summary>
        public Contest Contest { get; set; }
    }
}
