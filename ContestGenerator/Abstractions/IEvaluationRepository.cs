using ContestGenerator.Models.Contest;

namespace ContestGenerator.Abstractions
{
    public class Criteria { }
    public class Evaluation { }

    public interface IEvaluationRepository
    {
        Task<Criteria> Create(string name, Contest contest);
        
        Task Delete(Criteria criteria);

        Task<Evaluation> Add(Evaluation evaluation);

        Task<Evaluation> Edit(Evaluation evaluation);

        Task Delete(Evaluation evaluation);
    }
}
