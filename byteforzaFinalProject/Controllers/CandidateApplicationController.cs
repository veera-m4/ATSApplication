using byteforzaFinalProject.IRepo;
using Microsoft.AspNetCore.Mvc;

namespace byteforzaFinalProject.Controllers
{
	public class CandidateApplicationController : Controller
	{
		CandidateRepo candidateRepo;
		public CandidateApplicationController(CandidateRepo candidateRepo)
		{
			this.candidateRepo = candidateRepo;
		}

		public IActionResult Index()
		{
			ViewData["jobs"] = candidateRepo.jobDetails();

			return View();
		}
	}
}
