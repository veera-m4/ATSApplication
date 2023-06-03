using byteforzaFinalProject.DatabaseContext;
using byteforzaFinalProject.DTO;
using byteforzaFinalProject.Interface;
using byteforzaFinalProject.IRepo;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using byteforzaFinalProject.Models;
using byteforzaFinalProject.Response;
using Microsoft.DotNet.MSIdentity.Shared;

namespace byteforzaFinalProject.Controllers
{
	public class CandidateApplicationController : Controller
	{
		private readonly CandidateInterface candidateRepo;
        private readonly ApplicationDbContext db;
        private readonly IHttpContextAccessor httpContext;

        public CandidateApplicationController(CandidateInterface candidateRepo, ApplicationDbContext db , IHttpContextAccessor httpContext)
		{
			this.candidateRepo = candidateRepo;
            this.db = db;
            this.httpContext = httpContext;
		}

		public async Task<IActionResult> Index()
		{

            if(User.IsInRole("candidate"))
            {
                Console.WriteLine("yes");
            }
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == claims.Value);
            var email = currentUser.Email;
            List< AppliedJobDTO> appliedJob = candidateRepo.CandidateAppliedJob(email); 
            ViewData["appliedJob"] = appliedJob;
            List<JobDTO> job = await candidateRepo.jobDetails();
            ViewData["job"] = job;
			return View();
		}
        public async Task<IActionResult> AddCandidateForm(int jobId)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == claims.Value);
            var email = currentUser.Email;
            ViewBag.email=email;
            ViewBag.job = jobId;
            var tem = db.candidates.FirstOrDefault(x => x.Email == email);
            if(tem == null)
            {
                return View();
            }
            FormResponse response = await candidateRepo.AddCandidateProcess(jobId, email);
            if (response.result == "success")
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("JobDetails", jobId);
        }
        public IActionResult addProfileForm()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == claims.Value);
            string email = (string)currentUser.Email;
            ViewBag.email = email;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> addProfile(ProfileDTO profileDTO)
        {
            FormResponse response = await candidateRepo.addProfile(profileDTO);
            if(response.result == "success")
            {
                return Json(response); 
            }
            response.result = "failed";
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddCandidate(NewCandidateFormDTO candidateRegisterDTO)
        {
            FormResponse result = await candidateRepo.SaveCandidateDetailsAsync(candidateRegisterDTO);
            return Json(result);
        }
        public async Task<IActionResult> JobDetails(int id)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == claims.Value);
            string email = (string)currentUser.Email;
            Job job = await candidateRepo.getJobDetails(id);
            ViewData["job"] = job;
            ViewBag.applied = candidateRepo.isJobApplied(id, email);
            return View();
        }
    }
}
