using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace byteforzaFinalProject.Models
{
    public class Interview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string InterviewPanel { get; set; }
        [Required]
        public string typeOfRound { get; set;}
        [Required]
        public DateTime  InterviewDate { get; set; }
        [Required]
        public string Status { get; set; } = "scheduled";
        [ForeignKey("Candidate")] 
        public int CandidateId { get; set; }
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public virtual Candidate Candidate { get; set; }
        public virtual Job job { get; set; }
        public virtual List<Feedback> Feedbacks { get; set; }

    }
}
