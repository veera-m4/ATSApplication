namespace byteforzaFinalProject.DTO
{
    public class HomePageFeedback
    {
        public string Name { get; set; }
        public string Interviewer { get; set; }
        public string TypeOfRound { get; set; }
        public DateOnly DateOnly { get; set; }
        public TimeOnly TimeOnly { get; set; }
        public int Rating { get; set; }
        public bool halfstar { get; set; }
    }
}
