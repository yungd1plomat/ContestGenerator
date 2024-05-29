using ContestGenerator.Models.Contest;

namespace ContestGenerator.Abstractions
{
    public interface IContestRepository
    {
        Task<Contest> GetByName(string name);

        Task<Contest> GetByDomain(string domain);

        Task<Contest> Create(Contest contest);

        Task<Contest> Edit(string contestName, Contest contest);

        Task<Contest> Delete(string contestName);
    }
}
