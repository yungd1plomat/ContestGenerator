using System.ComponentModel.DataAnnotations;

namespace ContestGenerator.Models.File
{
    /// <summary>
    /// Класс предоставляет информацию
    /// о загруженном файле
    /// </summary>
    public class FileModel
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Путь файла
        /// </summary>
        public string Path { get; set; }
    }
}
