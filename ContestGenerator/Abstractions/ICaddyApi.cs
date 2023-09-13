using System.Text.Json.Nodes;

namespace ContestGenerator.Abstractions
{
    public interface ICaddyApi
    {
        /// <summary>
        /// Добавляет новый роут для указанного домена
        /// </summary>
        /// <param name="domain">Домен</param>
        Task AddNewRoute(string domain);

        /// <summary>
        /// Получает конфиг Caddy
        /// </summary>
        Task<JsonNode> GetConfig();

        /// <summary>
        /// Удаляет указанный роут
        /// </summary>
        /// <param name="index">Индекс хандлера который нужно удалить</param>
        Task DeleteRoute(int index);
    }
}
