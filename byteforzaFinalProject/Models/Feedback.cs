
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace byteforzaFinalProject.Models
{
	public class Feedback
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[ForeignKey("Interview")]
		[Required]
		public int interviewId { get; set; }
		[Required]
		public int oopsRating { get; set; }
		[Required]
		public int logicalThinking { get; set; }
		[Required]
		public int programming { get; set; }
		[Required]
		public string status { get; set; }
		[Required]
		public string comment { get; set; }
		public virtual Interview Interview { get; set; }
	}
}
