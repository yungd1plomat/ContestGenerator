using ContestGenerator.Models.Contest;
using ContestGenerator.Models.File;

namespace ContestGenerator.Models.Viewmodels
{
    public class CreateContestViewmodel
    {
        public IEnumerable<FileModel> Files { get; set; }

        public IEnumerable<Criteria> Criterias { get; set; }
    }
}
