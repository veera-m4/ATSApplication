using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace byteforzaFinalProject.Models
{
    public class CandidateProcess
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        [ForeignKey("Interview")]
        public int? Tech1 { get; set; }
        [ForeignKey("Interview")]
        public int? Tech2 { get; set; } = 0;
        [ForeignKey("Interview")]
        public int? Hr { get; set; } = 0;
        [Required]
        [ForeignKey("Job")]
        public int JobId { get; set; }
        [Required]
        public string status { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime updatedDate { get; set; }
        public virtual Candidate candidate { get; set; }
        

    }
}
