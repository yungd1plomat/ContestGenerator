﻿namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Класс содержащий все ответы
    /// пользователя
    /// </summary>
    public class Response
    {
        public int Id { get; set; }

        /// <summary>
        /// Все ответы пользователя
        /// </summary>
        public IList<FormResponse> Responses { get; set; }

        /// <summary>
        /// Конкурс на котором он отвечал
        /// </summary>
        public Contest Contest { get; set; }
    }
}
