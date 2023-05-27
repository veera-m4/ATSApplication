using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace byteforzaFinalProject.DTO
{
    public class NewJobDTO
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public int Vaccanies { get; set; } 
        public int ExperienceRequired { get; set; }
        public string Description { get; set; }
    }
}
