namespace ContestGenerator.Models
{
    public class DomainsViewmodel
    {
        public int Page { get; set; }

        public IEnumerable<Domain.Domain> Domains { get; set; }

        public IEnumerable<Contest.Contest> Contests { get; set; }
    }
}
