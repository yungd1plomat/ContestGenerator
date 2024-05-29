using ContestGenerator.Models.Contest;
using ContestGenerator.Models.Domain;

namespace ContestGenerator.Abstractions
{
    public interface IDomainRepository
    {
        Task<Domain> GetByName(string name);

        Task<Domain> Create(string domain, Contest contest);

        Task Remove(string domain);
    }
}
