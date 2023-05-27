namespace byteforzaFinalProject.DTO
{
    public class FeedbackPageDTO
    {
        public string Name { get; set; }
        public string Interviewer { get; set; }
        public string TypeOfRound { get; set; }
        public DateOnly DateOnly { get; set; }
        public TimeOnly TimeOnly { get; set; }
        public int Rating { get; set; }
        public bool halfstar { get; set; }
        public double RatingValue { get; set; }
        public string Email { get; set; }
    }
}
