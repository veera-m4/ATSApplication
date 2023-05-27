namespace byteforzaFinalProject.DTO
{
    public class CandidateProfileData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public List<string> KeySkills { get; set; }
        public int CurrentCTC { get; set; }
        public int ExpectedCTC { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int Notice { get; set; }
    }
}
