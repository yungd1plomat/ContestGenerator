namespace ContestGenerator.Models
{
    public class ContestsViewmodel
    {
        public int Page { get; set; }

        public IEnumerable<Contest.Contest> Contests { get; set; }
    }
}
