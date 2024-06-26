﻿using ContestGenerator.Models.Contest;
using ContestGenerator.Models.File;

namespace ContestGenerator.Models
{
    public class EditViewmodel
    {
        public Contest.Contest Contest { get; set; }


        public IEnumerable<FileModel> Files { get; set; }

        public IEnumerable<Criteria> Criterias { get; set; }
    }
}
