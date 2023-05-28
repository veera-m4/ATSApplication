using byteforzaFinalProject.DTO;
using byteforzaFinalProject.Interface;
using byteforzaFinalProject.Models;
using byteforzaFinalProject.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Data;
using System.Diagnostics;

namespace byteforzaFinalProject.Controllers
{
    public class ATSApplicationController : Controller
    {
        CandidateInterface candidateRepo;
        public ATSApplicationController(CandidateInterface candidateRepo)
        {
            this.candidateRepo = candidateRepo;
        }

        public async Task<ActionResult> CandidateDetail()
        {
            List<Object> graphData = candidateRepo.graphData();
			ViewData["data"] = new ShowCandidateDetails() {
                total_of_candidate = (int)graphData[1],
                candidate_selected = (int)graphData[2],
                candidate_on_process = (int)graphData[4],
                candidate_Rejected = (int)graphData[4]
			};
            ViewData["job"] = await candidateRepo.jobDetails();
            ViewData["candidate"] = candidateRepo.GetCandidatePageAsync(); 
               
             ViewData["graph"] = new List<Object> { graphData[2] , graphData[3] , graphData[4] };
            return View();
        }
        [HttpGet]
        public JsonResult graphData()
        {
            List<Object> graphData = candidateRepo.graphData();
            return Json(new List<Object> { graphData[2], graphData[3], graphData[4] });
        }
        [HttpGet]
        public JsonResult HomePagegraphData()
        {
            List<Object> graphData = candidateRepo.graphData();
            return Json(new List<Object> { graphData[8], graphData[2], graphData[3], graphData[4], graphData[5], graphData[6], graphData[7] });
        }
        public IActionResult ReportPage()
        {
            ViewData["interviewReport"] = candidateRepo.getInterviewReport();
            ViewData["InrerviewSchedule"] = candidateRepo.getTheScheduledInterview();
            ViewData["PageDetails"] = candidateRepo.GetReportPageDetails();
            return View();
        }
        public IActionResult HomePage()
        {
            ViewData["InrerviewSchedule"] = candidateRepo.getTheScheduledInterview();
            ViewData["candidate"] = candidateRepo.GetCandidatePageAsync();
            ViewData["feedback"] = candidateRepo.getFeedBackPageDetails();
            return View();
        }
        public IActionResult FeedbackPage()
        {
            ViewData["feedback"] = candidateRepo.getFeedBackPageDetails();
            return View();
        }
        public IActionResult AddJobForm()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> AddJob( NewJobDTO newJob)
        {
            return Json(await candidateRepo.AddJobAsynac(newJob));
        }
        public async Task<IActionResult> DeleteJob(int Id)
        {
            await candidateRepo.DeleteJobDetails(Id);
            return RedirectToAction("CandidateDetail");
        }
        public async Task<IActionResult> EditJobForm(int Id) {
            Job job = await candidateRepo.getJobDetails(Id);
            ViewData["job"] = job;
            return View(); 
        }
        public async Task<IActionResult> EditJob(NewJobDTO newJob)
        {
            return Json(await candidateRepo.EditJobDetails(newJob));
        }
        public async Task<IActionResult> JobDetails(int id)
        {
            Job job = await candidateRepo.getJobDetails(id);
			ViewData["job"] = job;
            return View();
        }
        public async Task<IActionResult> IsEmailAlreadyExists(string email)
        {
            return Ok(candidateRepo.IsEmailAlreadyExists(email));
        }
        public async  Task<IActionResult> AddCandidateForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCandidate(NewCandidateFormDTO candidateRegisterDTO)
        {
            candidateRepo.SaveCandidateDetailsAsync(candidateRegisterDTO);
            return RedirectToAction("AddCandidateForm");
        }
    }
}
