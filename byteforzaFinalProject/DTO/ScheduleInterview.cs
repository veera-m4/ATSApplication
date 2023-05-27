namespace byteforzaFinalProject.DTO
{
	public class ScheduleInterview
	{
		public string InterviewPanel { get; set; }
		public string type_of_round { get; set; }
		public DateOnly date_only { get; set; }
		public TimeOnly TimeOnly { get; set; }
		public int jobId { get; set; }
		public int candidateId { get; set; }

	}
}
