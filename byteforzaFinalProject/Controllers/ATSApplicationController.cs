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
            ViewData["data"] = new ShowCandidateDetails() {
                total_of_candidate = 30,
                candidate_selected = 12,
                candidate_on_process = 8,
                candidate_Rejected = 10
            };
            ViewData["job"] = await candidateRepo.jobDetails();
            ViewData["candidate"] = candidateRepo.GetCandidatePageAsync(); 
               
             ViewData["graph"] = new List<int> { 12,8,2};
            return View();
        }
        [HttpGet]
        public JsonResult graphData()
        {
            return Json(new List<int> { 51, 52, 38 });
        }
        [HttpGet]
        public JsonResult HomePagegraphData()
        {
            return Json(new List<Object> { 78.2,51, 52, 38, 51, 52, 38 });
        }
        public IActionResult ReportPage()
        {
            ViewData["interviewReport"] = new List<InterviewReportDetails>()
            {
                new InterviewReportDetails()
                {
                    Name = "Veera manikandan",
                    InterviewPanel = "manikandan",
                    result = "selected",
                    type_of_round = "tr"
                },
                new InterviewReportDetails()
                {
                    Name = "Veera manikandan",
                    InterviewPanel = "manikandan",
                    result = "selected",
                    type_of_round = "tr"
                },
                new InterviewReportDetails()
                {
                    Name = "Veera manikandan",
                    InterviewPanel = "manikandan",
                    result = "selected",
                    type_of_round = "tr"
                },
                new InterviewReportDetails()
                {
                    Name = "Veera manikandan",
                    InterviewPanel = "manikandan",
                    result = "selected",
                    type_of_round = "tr"

                },
                new InterviewReportDetails()
                {
                    Name = "Veera manikandan",
                    InterviewPanel = "manikandan",
                    result = "selected",
                    type_of_round = "tr"
                },
                new InterviewReportDetails()
                {
                    Name = "Veera manikandan",
                    InterviewPanel = "manikandan",
                    result = "selected",
                    type_of_round = "tr"
                },
                new InterviewReportDetails()
                {
                    Name = "Veera manikandan",
                    InterviewPanel = "manikandan",
                    result = "selected",
                    type_of_round = "tr"
                }
            };
            ViewData["InrerviewSchedule"] = new List<InterviewScheludeDetail>()
            {
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 }
            };
            ViewData["PageDetails"] = new ReportPageDetails()
            {
                total_of_candidate = 40,
                totalInterview = 60,
                candidate_on_process = 20,
                candidate_selected = 12,
                candidate_Rejected = 8
            };
            return View();
        }
        public IActionResult HomePage()
        {
            ViewData["InrerviewSchedule"] = new List<InterviewScheludeDetail>()
            {
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 },
                new InterviewScheludeDetail()
                {
                    Name = "veera Manikandan",
                    InterviewPanel = "manikandan",
                    type_of_round = "tr",
                    date_only = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0)
                 }
            };
            ViewData["candidate"] = candidateRepo.GetCandidatePageAsync();
            ViewData["feedback"] = new List<HomePageFeedback>() {
                new HomePageFeedback()
                {
                    Name = "veera Manikandan",
                    Interviewer = "manikandan",
                    TypeOfRound = "tr",
                    DateOnly = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0),
                    Rating = 3,
                    halfstar = true
                },
                new HomePageFeedback()
                {
                    Name = "veera Manikandan",
                    Interviewer = "manikandan",
                    TypeOfRound = "tr",
                    DateOnly = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0),
                    Rating = 3,
                    halfstar = true
                },
                new HomePageFeedback()
                {
                    Name = "veera Manikandan",
                    Interviewer = "manikandan",
                    TypeOfRound = "tr",
                    DateOnly = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0),
                    Rating = 3,
                    halfstar = true
                },
                new HomePageFeedback()
                {
                    Name = "veera Manikandan",
                    Interviewer = "manikandan",
                    TypeOfRound = "tr",
                    DateOnly = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0),
                    Rating = 3,
                    halfstar = true
                },
                new HomePageFeedback()
                {
                    Name = "veera Manikandan",
                    Interviewer = "manikandan",
                    TypeOfRound = "tr",
                    DateOnly = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0),
                    Rating = 3,
                    halfstar = true
                },
                new HomePageFeedback()
                {
                    Name = "veera Manikandan",
                    Interviewer = "manikandan",
                    TypeOfRound = "tr",
                    DateOnly = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0),
                    Rating = 3,
                    halfstar = true
                }
            };
            return View();
        }
        public IActionResult FeedbackPage()
        {
            ViewData["feedback"] = new List<FeedbackPageDTO>() {
                new FeedbackPageDTO()
                {
                    Name = "veera Manikandan",
                    Interviewer = "manikandan",
                    TypeOfRound = "tr",
                    DateOnly = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0),
                    Rating = 3,
                    halfstar = true,
                    RatingValue = 3.8,
                    Email = "sutharsonv@gmail.com"
                },
                new FeedbackPageDTO()
                {
                    Name = "veera Manikandan",
                    Interviewer = "manikandan",
                    TypeOfRound = "tr",
                    DateOnly = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0),
                    Rating = 3,
                    halfstar = true,
                    RatingValue = 3.8,
                    Email = "manikandanv847@gmail.com"
                },
                new FeedbackPageDTO()
                {
                    Name = "veera Manikandan",
                    Interviewer = "manikandan",
                    TypeOfRound = "tr",
                    DateOnly = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0),
                    Rating = 3,
                    halfstar = true,
                    RatingValue = 3.8,
                    Email = "rubitha@gmail.com"
                },
                new FeedbackPageDTO()
                {
                    Name = "veera Manikandan",
                    Interviewer = "manikandan",
                    TypeOfRound = "tr",
                    DateOnly = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0),
                    Rating = 3,
                    halfstar = true,
                    RatingValue = 3.8,
                    Email = "ruby@gmail.com"
                },
                new FeedbackPageDTO()
                {
                    Name = "veera Manikandan",
                    Interviewer = "manikandan",
                    TypeOfRound = "tr",
                    DateOnly = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0),
                    Rating = 3,
                    halfstar = true,
                    RatingValue = 3.8,
                    Email = "veera@gmail.com"
                },
                new FeedbackPageDTO()
                {
                    Name = "veera Manikandan",
                    Interviewer = "manikandan",
                    TypeOfRound = "tr",
                    DateOnly = new DateOnly(2022, 1, 1),
                    TimeOnly = new TimeOnly(11, 0),
                    Rating = 3,
                    halfstar = true,
                    RatingValue = 3.8,
                    Email = "mani@gmail.com"
                }
            };
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
