namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Класс предоставляет инфорацию о разделе
    /// помощь участнику, может содержать такие данные как -
    /// прошлые победители, критерии оценки и др.
    /// </summary>
    public class Help
    {
        public int? Id { get; set; }

        /// <summary>
        /// Название раздела
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Содержание раздела
        /// </summary>
        public string Description { get; set; }
    }
}
