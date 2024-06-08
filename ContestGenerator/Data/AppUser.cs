using ContestGenerator.Models.Contest;
using Microsoft.AspNetCore.Identity;

namespace ContestGenerator.Data
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Все ответы пользователя
        /// </summary>
        public List<Response> Responses { get; set; } 
    }
}
