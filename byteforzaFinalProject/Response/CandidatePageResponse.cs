using byteforzaFinalProject.DTO;

namespace byteforzaFinalProject.Response
{
    public class CandidatePageResponse
    {
        public string result { get; set; }
        public string Error { get; set; } = null;
        public ShowCandidateDetails ShowCandidateDetails { get; set; }
    }
}
