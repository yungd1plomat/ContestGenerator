namespace ContestGenerator.Abstractions
{
    public interface ICaddyApi
    {
        /// <summary>
        /// Добавляет новый роут для указанного домена
        /// </summary>
        /// <param name="domain">Домен</param>
        /// <param name="contestName">К какой олимпиаде будет перенаправляться</param>
        Task AddNewRoute(string domain, string contestName);
    }
}
