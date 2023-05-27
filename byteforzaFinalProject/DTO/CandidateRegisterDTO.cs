using System.ComponentModel.DataAnnotations;

namespace byteforzaFinalProject.DTO
{
	public class CandidateRegisterDTO
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string password { get; set; }
		[Required]
		public string Role { get; set; }
	}
}
