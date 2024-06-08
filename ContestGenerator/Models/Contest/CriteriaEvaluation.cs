namespace ContestGenerator.Models.Contest
{
    /// <summary>
    /// Содержит оценку по определенному критерию для
    /// олимпиады
    /// </summary>
    public class CriteriaEvaluation
    {
        public int Id { get; set; }

        /// <summary>
        /// Критерий по которому проводится оценка
        /// </summary>
        public Criteria Criteria { get; set; }

        /// <summary>
        /// Оценка по заданному критерию
        /// </summary>
        public double Evaluation { get; set; }
    }
}
