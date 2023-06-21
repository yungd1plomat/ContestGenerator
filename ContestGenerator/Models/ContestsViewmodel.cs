namespace ContestGenerator.Models
{
    public class ContestsViewmodel
    {
        public int Page { get; set; }

        public IEnumerable<ContestInfo> Contests { get; set; }
    }
}
