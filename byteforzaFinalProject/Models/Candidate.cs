using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace byteforzaFinalProject.Models
{
    public class Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Experience { get; set; }
        [Required]
        public string KeySkills { get; set; }
        [Required]
        public int CurrentCTC { get; set; }
        [Required]
        public int ExpectedCTC { get; set; }
        [Required]
        [Remote("IsEmailAlreadyExists", "ATSApplication",ErrorMessage ="userAlready exits")]
        public string Email { get; set; }
        [Required]
        public string Resume { get; set; }
        [Required]
        public int NoticePeriod { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string PreferLocation { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public string Note { get; set; }
        public virtual ICollection<CandidateProcess> Child1 { get; set; }
        public virtual IList<Interview> Child2 { get;set; }


    }
}
