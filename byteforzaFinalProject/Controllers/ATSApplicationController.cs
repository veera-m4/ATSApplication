using byteforzaFinalProject.DTO;
using byteforzaFinalProject.Interface;
using byteforzaFinalProject.IRepo;
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
		private readonly CandidateInterface candidateRepo;
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
        public IActionResult ScheduleInterviewList()
        {
            List<InterviewListDTO> interviewLists = candidateRepo.getListForSchedule();
            return Json(interviewLists);
        }
        public IActionResult getInterviewPage()
        {
            return View();
        }
        public IActionResult IndividualCandidateDetails(int candidateId , int jobId)
        {
            ViewData["individual"] = candidateRepo.getIndividualDetails(candidateId, jobId);
            return View();
        }
        public async Task<IActionResult> ApproveApplication(int candidateId, int jobId)
        {
            candidateRepo.approveApplication(candidateId, jobId);
            return RedirectToAction("CandidateDetail");
        }
        public async Task<IActionResult> DeclineApplication(int candidateId, int jobId)
        {
            candidateRepo.declineApproval(candidateId, jobId);
            return RedirectToAction("CandidateDetail");
        }
        public IActionResult AddNewInterview()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewInterviewDetails(ScheduleInterview scheduleInterview)
        {
            FormResponse formResponse = (await candidateRepo.scheduleInterview(scheduleInterview));
            return Json(formResponse);
        }
        public IActionResult AddFeedbackForm()
        {
            return View();
        }
        public IActionResult getNewFeedbackList()
        {
            return Json(candidateRepo.getNewFeedbackList());
        }
        public async Task<IActionResult> AddNewFeedback(AddFeedback addFeedback)
        {
            return Ok(Json(await candidateRepo.AddFeedBack(addFeedback));
        }
    }
}
