using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace byteforzaFinalProject.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Role { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Vaccanies { get; set; }
        [Required]
        public int Selected { get; set; }
        [Required]
        public int OnProcess { get; set; }
        [Required]
        public int ExperienceRequired { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
        public string status { get; set; }
        public virtual ICollection<CandidateProcess> Child1 { get; set; }
        public virtual ICollection<Interview> Child2 { get; set; }
    }

}
