using ContestGenerator.Models.File;

namespace ContestGenerator.Models
{
    public class FileListViewmodel
    {
        public int Page { get; set; }

        public IEnumerable<FileModel> Files { get; set; }
    }
}
