namespace byteforzaFinalProject.DTO
{
    public class SingleFeedbackDetails
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set;}
        public string Role { get; set; }
        public int jobId { get; set; }
        public string InterviewPanel { get; set; }
        public string TypeOfRound { get; set; }
        public DateOnly dateOnly { get; set; }
        public TimeOnly timeOnly { get; set; }
        public int oops { get; set; }
        public int programming { get; set; }
        public int logicalThinking { get; set; }
        public string comments { get; set; }
        public string status { get; set; }
    }
}
