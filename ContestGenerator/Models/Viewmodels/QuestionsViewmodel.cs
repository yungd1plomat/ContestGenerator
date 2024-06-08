using ContestGenerator.Models.Contest;

namespace ContestGenerator.Models
{
    public class QuestionsViewmodel
    {
        public int Page { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}
