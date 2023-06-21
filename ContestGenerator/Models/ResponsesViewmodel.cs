using ContestGenerator.Models.Contest;

namespace ContestGenerator.Models
{
    public class ResponsesViewmodel
    {
        public int Page { get; set; }

        public IEnumerable<Response> Responses { get;set; }
    }

}
