using ContestGenerator.Models.File;

namespace ContestGenerator.Models
{
    public class EditViewmodel
    {
        public Contest.Contest Contest { get; set; }


        public IEnumerable<FileModel> Files { get; set; }
    }
}
