namespace byteforzaFinalProject.DTO
{
    public class ProfileDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Experience { get; set; }
        public List<string> Skill { get; set; }
        public int currentCTC { get; set; }
        public int expectedCTC { get; set; }
        public string currentLocation { get; set; }
        public string preferedLocation { get; set; }
        public string source { get; set; }
        public IFormFile Resume { get; set; }
        public int NoticePeriod { get; set; }
    }
}
