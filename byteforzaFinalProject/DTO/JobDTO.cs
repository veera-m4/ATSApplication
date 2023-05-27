using System.ComponentModel.DataAnnotations;

namespace byteforzaFinalProject.DTO
{
    public class JobDTO
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public int Applied { get; set; }
        public int Vaccanies { get; set; }
        public int Selected { get; set; }
        public int OnProcess { get; set; }
        public int ExperienceRequired { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Description { get;set; }
    }
}
