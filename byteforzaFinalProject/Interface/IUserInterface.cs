using byteforzaFinalProject.DTO;
using byteforzaFinalProject.Response;

namespace byteforzaFinalProject.Interface
{
	public interface IUserInterface
	{
		Task<FormResponse> LoginAsync(LoginDTO model);
		Task LogoutAsync();
		Task<FormResponse> RegisterAsync(CandidateRegisterDTO model);
	}
}
