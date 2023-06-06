using byteforzaFinalProject.DatabaseContext;
using byteforzaFinalProject.DTO;
using byteforzaFinalProject.Interface;
using byteforzaFinalProject.Models;
using byteforzaFinalProject.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace byteforzaFinalProject.IRepo
{
    public class CandidateRepo : CandidateInterface
    {
        ApplicationDbContext db;
        IFileInterface fileRepo;
		public CandidateRepo(ApplicationDbContext db, IFileInterface file)
        {
            this.db = db;
            this.fileRepo = file;
        }
        public async Task<FormResponse> AddJobAsynac(NewJobDTO newJobDTO)
        {
            FormResponse formResponse = new FormResponse();
            try
            {

                Job newJob = new Job();
                newJob.Location = newJobDTO.Location;
                newJob.Role = newJobDTO.Role;
                newJob.Selected = 0;
                newJob.Vaccanies = newJobDTO.Vaccanies;
                newJob.Description = newJobDTO.Description;
                newJob.OnProcess = 0;
                newJob.ExperienceRequired = newJobDTO.ExperienceRequired;
                newJob.Type = newJobDTO.Type;
                newJob.PostedDate = DateTime.Today;
                newJob.status = "recruiting";
                await db.jobs.AddAsync(newJob);
                await db.SaveChangesAsync();
                formResponse.Message = null;
                formResponse.Error = null;
                formResponse.result = "success";
            }
            catch (Exception ex)
            {
                formResponse.Message = null;
                formResponse.Error = ex.Message;
                formResponse.result = "failed";
            }
            return formResponse;
        }
        public async Task<FormResponse> EditJobDetails(NewJobDTO newJob)
        {
            FormResponse formResponse = new FormResponse();
            Job job = await db.jobs.FirstOrDefaultAsync(x => x.Id == newJob.Id);
            if (job == null)
            {
                formResponse.result = "failed";
                formResponse.Message = "job can't found";
                return formResponse;
            }
            job.Location = newJob.Location;
            job.ExperienceRequired = newJob.ExperienceRequired;
            job.Description = newJob.Description;
            job.Role = newJob.Role;
            job.Type = newJob.Type;
            job.Vaccanies= newJob.Vaccanies;
            await db.SaveChangesAsync();
            formResponse.result = "success";
            return formResponse;
        }
        public async Task<FormResponse> DeleteJobDetails(int i)
        {
            FormResponse formResponse = new FormResponse();
            Job job = await db.jobs.FirstOrDefaultAsync(x => x.Id == i);
            if (job == null)
            {
                formResponse.result = "failed";
                formResponse.Message = "job can't found";
                return formResponse;
            }
            db.jobs.Remove(job);

            await db.SaveChangesAsync();
            formResponse.result = "succeed";
            return formResponse;
        }
        public async Task<List<JobDTO>> jobDetails()
        {
            List<Job> jobList = await db.jobs.ToListAsync();
            List<JobDTO> result = new List<JobDTO>();
            foreach(Job j in jobList)
            {
                result.Add(new JobDTO
                {

                    Id = j.Id,
                    Role = j.Role,
                    Location = j.Location,
                    Type = j.Type,
                    Vaccanies = j.Vaccanies,
                    ExperienceRequired = j.ExperienceRequired,
                    Description = j.Description,
                    Selected = j.Selected,
                    OnProcess = j.OnProcess,
                    Applied = (from cp in db.candidatesProcess where cp.JobId == j.Id select cp).ToList().Count()
                }); ;
            }
            return result;
        }
        public async Task<Job> getJobDetails(int i)
        {
            Job job = await db.jobs.FirstOrDefaultAsync(a => a.Id == i);
            return job;
        }
        public Task<CandidatePageResponse> DeleteCandidateDetailsAsync(int candidateId)
        {
            throw new NotImplementedException();
        }

        public Task<CandidatePageResponse> EditCandidateDetailsAsync(Candidate candidate)
        {
            throw new NotImplementedException();
        }

        public List<CandidateProfileData> GetCandidatePageAsync()
        {
            List< CandidateProfileData> listData = (from c in db.candidates
                           join cp in db.candidatesProcess on c.Id equals cp.CandidateId
                           join j in db.jobs on cp.JobId equals j.Id
                           select new CandidateProfileData {
                               Role = j.Role,
                               Name = c.Name,
                               Id = c.Id,
                               JobId = cp.JobId,
                               Experience = c.Experience,
                               KeySkills = (c.KeySkills).Split(',', System.StringSplitOptions.None).ToList(),
			                   CurrentCTC = c.CurrentCTC,
							   ExpectedCTC = c.ExpectedCTC,
							   Email = c.Email,
							   Status = cp.status,
							   Notice = c.NoticePeriod
						   }).ToList();
            return listData;
        }

        public bool IsEmailAlreadyExists(string email)
        {
            return db.candidates.Any(x => x.Email == email);
        }

        public async Task<FormResponse> SaveCandidateDetailsAsync(NewCandidateFormDTO candidate)
        {
            FormResponse formResponse = new FormResponse();
			Candidate profileData = await db.candidates.FirstOrDefaultAsync(x => x.Email == candidate.Email);
            CandidateProcess candidateProcess = null;

			if (profileData != null)
            {
				candidateProcess = await db.candidatesProcess.FirstOrDefaultAsync(x => x.JobId == candidate.JobId && profileData.Id == x.CandidateId);
			}
            
            if(candidateProcess != null) {
                formResponse.result = "failed";
                formResponse.Message = "You have alreadry applied for job";
                return formResponse;
            }
            profileData = new Candidate();
            profileData.Name = candidate.Name;
            profileData.Email = candidate.Email;
            profileData.CurrentCTC = candidate.currentCTC;
            profileData.NoticePeriod = candidate.NoticePeriod;
            profileData.ExpectedCTC = candidate.expectedCTC;
            profileData.Resume = await fileRepo.saveAndGetFileName(candidate.Resume);
            profileData.Experience = candidate.Experience;
            profileData.KeySkills = string.Join(",", candidate.Skill.ToArray());
            profileData.Location = candidate.currentLocation;
            profileData.PreferLocation = candidate.preferedLocation;
            profileData.Source = candidate.source;
            profileData.Note = "";
            await db.candidates.AddAsync(profileData);
            
            await db.SaveChangesAsync();
            Thread.Sleep(5000);
            await AddCandidateProcess(candidate.JobId, candidate.Email);
            formResponse.result = "success";
            formResponse.Message = "Suucessfully applied  for the job";
			return formResponse;

		}
        public async Task<FormResponse> AddCandidateProcess(int jobId, string email)
        {
            FormResponse formResponse = new FormResponse();
            Candidate candidate = db.candidates.FirstOrDefault(x => x.Email == email);
            CandidateProcess candidateProcess1 = new CandidateProcess();
            candidateProcess1.CandidateId = candidate.Id;
            candidateProcess1.JobId = jobId;
            candidateProcess1.AppliedDate = DateTime.Today;
            candidateProcess1.updatedDate = DateTime.Today;
            candidateProcess1.status = "applied";
            await db.candidatesProcess.AddAsync(candidateProcess1);
            await db.SaveChangesAsync();
            formResponse.result = "success";
            formResponse.Message = "Suucessfully applied  for the job";
            return formResponse;
        }

        public async Task<FormResponse> addProfile(ProfileDTO candidate)
        {
            FormResponse formResponse = new FormResponse();
            Candidate profileData = await db.candidates.FirstOrDefaultAsync(x => x.Email == candidate.Email);

            if (profileData != null)
            {
                profileData.Name = candidate.Name;
                profileData.Email = candidate.Email;
                profileData.CurrentCTC = candidate.currentCTC;
                profileData.NoticePeriod = candidate.NoticePeriod;
                profileData.ExpectedCTC = candidate.expectedCTC;
                profileData.Resume = await fileRepo.saveAndGetFileName(candidate.Resume);
                profileData.Experience = candidate.Experience;
                profileData.KeySkills = string.Join(",", candidate.Skill.ToArray());
                profileData.Location = candidate.currentLocation;
                profileData.PreferLocation = candidate.preferedLocation;
                profileData.Source = candidate.source;
                profileData.Note = "";
                await db.SaveChangesAsync();
                formResponse.result = "success";
                formResponse.Message = "Suucessfully applied  for the job";
                return formResponse;
            }

            profileData = new Candidate();
            profileData.Name = candidate.Name;
            profileData.Email = candidate.Email;
            profileData.CurrentCTC = candidate.currentCTC;
            profileData.NoticePeriod = candidate.NoticePeriod;
            profileData.ExpectedCTC = candidate.expectedCTC;
            profileData.Resume = await fileRepo.saveAndGetFileName(candidate.Resume);
            profileData.Experience = candidate.Experience;
            profileData.KeySkills = string.Join(",", candidate.Skill.ToArray());
            profileData.Location = candidate.currentLocation;
            profileData.PreferLocation = candidate.preferedLocation;
            profileData.Source = candidate.source;
            profileData.Note = "";
            await db.candidates.AddAsync(profileData);
            await db.SaveChangesAsync();
            formResponse.result = "success";
            formResponse.Message = "Suucessfully applied  for the job";
            return formResponse;
        }

        public async Task<FormResponse> scheduleInterview(ScheduleInterview scheduleInterview)
		{
            FormResponse formResponse = new FormResponse();
			Interview interview = new Interview();
            interview.InterviewDate = scheduleInterview.dateTime;
			interview.InterviewPanel = scheduleInterview.InterviewPanel;
			interview.CandidateId = scheduleInterview.candidateId;
			interview.JobId = scheduleInterview.jobId;
            string type_of_round = (from cp in db.candidatesProcess
                                    where cp.CandidateId == scheduleInterview.candidateId && cp.JobId == scheduleInterview.jobId
                                    select cp.status).FirstOrDefault();
            interview.typeOfRound = type_of_round == "tr1" ? "technical 1" : (type_of_round == "tr1" ? "technical 2" : "general hr") ;

            await db.interviews.AddAsync(interview);
            await db.SaveChangesAsync();
            Thread.Sleep(5000);
            CandidateProcess candidateProcess = db.candidatesProcess.FirstOrDefault(x => x.CandidateId == scheduleInterview.candidateId && x.JobId == scheduleInterview.jobId);
            if(candidateProcess.status == "tr1")
            {
                candidateProcess.Tech1 = db.interviews.FirstOrDefault(x => x.typeOfRound == scheduleInterview.type_of_round && x.CandidateId == scheduleInterview.candidateId && x.JobId == scheduleInterview.jobId).Id;
            }
            else if (candidateProcess.status == "tr2")
			{
				candidateProcess.Tech2 = db.interviews.FirstOrDefault(x => x.typeOfRound == scheduleInterview.type_of_round && x.CandidateId == scheduleInterview.candidateId && x.JobId == scheduleInterview.jobId).Id;
			}
            else if (candidateProcess.status == "hr")
			{
				candidateProcess.Hr = db.interviews.FirstOrDefault(x => x.typeOfRound == scheduleInterview.type_of_round && x.CandidateId == scheduleInterview.candidateId && x.JobId == scheduleInterview.jobId).Id;
			}
			await db.SaveChangesAsync();
			formResponse.result = "success";
            return formResponse;


	    }
        public async Task<FormResponse> AddFeedBack(AddFeedback addFeedback)
        {
            FormResponse formResponse = new FormResponse();
            Feedback feedback = new Feedback();
            feedback.logicalThinking = addFeedback.logicalThinking;
            feedback.programming = addFeedback.programming;
            feedback.oopsRating = addFeedback.oopsRating;
            feedback.status = addFeedback.status;
            feedback.comment = addFeedback.comment;
            feedback.interviewId = addFeedback.interviewId;
            Interview interview = (from i in db.interviews
                                  where i.Id == addFeedback.interviewId
                                  select i).FirstOrDefault();
            CandidateProcess candidateProcess = (from cp in db.candidatesProcess
                                                 where cp.CandidateId == interview.CandidateId && cp.JobId == interview.JobId
                                                 select cp).FirstOrDefault();
            if(addFeedback.status == "selected")
            {
				if (candidateProcess.status == "tr1")
				{
                    candidateProcess.status = "tr2";
                    interview.Status = "selected";
				}
				else if (candidateProcess.status == "tr2")
				{
					candidateProcess.status = "hr";
					interview.Status = "selected";
				}
                else if (candidateProcess.status == "hr")
				{
                    Job job = (from j in db.jobs
                               where j.Id == interview.JobId
                               select j).FirstOrDefault();
                    job.Selected += 1;
					candidateProcess.status = "offer raised";
					interview.Status = "selected";
				}
			}
            else
            {
                Job job = (from j in db.jobs
                           where j.Id == interview.JobId
                           select j).FirstOrDefault();
                job.OnProcess -= 1;
                candidateProcess.status = "rejected";
				interview.Status = "rejected";
			}
            await db.feedbacks.AddAsync(feedback);
            await db.SaveChangesAsync();
			formResponse.result = "success";
			return formResponse;

        }
        public List<FeedbackPageDTO> getFeedBackPageDetails()
        {
            List<FeedbackPageDTO> interviewScheludeDetails = (from f in db.feedbacks
                                                              join i in db.interviews on f.interviewId equals i.Id
                                                              join j in db.jobs on i.JobId equals j.Id
                                                              join c in db.candidates on i.CandidateId equals c.Id
                                                              select new FeedbackPageDTO
                                                              {
                                                                  jobName = j.Role,
                                                                  id = f.Id,
                                                                  candidateId = c.Id,
                                                                  Name = c.Name,
                                                                  Interviewer = i.InterviewPanel,
                                                                  TypeOfRound = i.typeOfRound,
                                                                  dateOnly = DateOnly.FromDateTime(i.InterviewDate),
                                                                  TimeOnly = new TimeOnly(i.InterviewDate.Hour, i.InterviewDate.Minute),
                                                                  Rating = (f.logicalThinking + f.oopsRating + f.programming) / 3,
                                                                  halfstar = ((int)((double)(f.logicalThinking + f.oopsRating + f.programming) / 3)*10) > 5 ? true :false,
                                                                  RatingValue = (float)(f.logicalThinking + f.oopsRating + f.programming) / 3.0,
                                                                  Email = c.Email,
                                                                  

															  }).ToList();
            return interviewScheludeDetails;
		}
        public List<Object> graphData()
        {
            
            int totalCandidate = db.candidatesProcess.Count();
            int selectedCandidate = db.candidatesProcess.Where(x => x.status == "offer raised").Count();
            int rejectedCandidate = db.candidatesProcess.Where(x => x.status == "rejected" || x.status == "will not Suit").Count();
            int pendingCandidate = totalCandidate - (selectedCandidate + rejectedCandidate);
            int thisWeekAppliedCandidate = (from i in db.candidatesProcess where i.updatedDate > DateTime.Now.AddDays(-7) && i.updatedDate < DateTime.Now select i).Count();
			int thisWeekSelectedCandidate = (from i in db.candidatesProcess where i.updatedDate > DateTime.Now.AddDays(-7) && i.updatedDate < DateTime.Now && i.status == "offer raised" select i).Count();
			int thisWeekRejecctedCandidate = (from i in db.candidatesProcess where i.updatedDate > DateTime.Now.AddDays(-7) && i.updatedDate < DateTime.Now && i.status == "not siuted" || i.status == "rejected" select i).Count();
            int thiWeekPendingCandidate = (from i in db.candidatesProcess where i.updatedDate > DateTime.Now.AddDays(-7) && i.updatedDate < DateTime.Now && i.status != "offer raised" && i.status != "not siuted" && i.status != "rejected" select i).Count();
			double completedPercentagge = ((double) selectedCandidate/ (double)db.jobs.Sum(x => x.Vaccanies)) * 100;
            List<Object> result = new List<Object>()
            {
                thisWeekAppliedCandidate,totalCandidate,selectedCandidate,pendingCandidate,rejectedCandidate,thisWeekSelectedCandidate, thiWeekPendingCandidate,thisWeekRejecctedCandidate, completedPercentagge
			};
            return result;
		}
        public List<InterviewScheludeDetail> getTheScheduledInterview()
        {
            List<InterviewScheludeDetail> result = (from c in db.candidates
                                                   join i in db.interviews on c.Id equals i.CandidateId
                                                   join j in db.jobs on i.JobId equals j.Id
                                                   where i.InterviewDate >= DateTime.Now
                                                   select new InterviewScheludeDetail
                                                   {
                                                       Role = j.Role,
                                                       Name = c.Name,
                                                       InterviewPanel = i.InterviewPanel,
                                                       type_of_round = i.typeOfRound,
                                                       date_only = DateOnly.FromDateTime(i.InterviewDate),
                                                       TimeOnly = new TimeOnly(i.InterviewDate.Hour, i.InterviewDate.Minute)
                                                   }).ToList();
            return result;
        }
        public List<InterviewReportDetails> getInterviewReport()
        {
            List<InterviewReportDetails> result = (from c in db.candidates
                                                   join i in db.interviews on c.Id equals i.CandidateId
                                                   join j in db.jobs on i.JobId equals j.Id
                                                   where i.InterviewDate <= DateTime.Now
                                                   select new InterviewReportDetails
                                                   {
                                                       Name = c.Name,
                                                       jobName = j.Role,
                                                       InterviewPanel = i.InterviewPanel,
                                                       result = i.Status,
                                                       type_of_round = i.typeOfRound,
                                                       

                                                   }).ToList();
            return result;
        }
        public ReportPageDetails GetReportPageDetails()
        {
            ReportPageDetails result = new ReportPageDetails();
            result.candidate_Rejected = db.candidatesProcess.Where(x => x.status == "rejected" || x.status == "not Suit").Count(); 
            result.candidate_selected = db.candidatesProcess.Where(x => x.status == "offer raised").Count(); 
            result.total_of_candidate = db.candidatesProcess.Count();
            result.candidate_on_process = result.total_of_candidate - (result.candidate_Rejected + result.candidate_selected);
            result.totalInterview = db.interviews.Count();
            return result;
        }
        public List<AppliedJobDTO> CandidateAppliedJob(string email)
        {
            List<AppliedJobDTO> result = (from c in db.candidates
                     join cp in db.candidatesProcess on c.Id equals cp.CandidateId
                     join j in db.jobs on cp.JobId equals j.Id
                     where c.Email == email
                      select new AppliedJobDTO
                      {

						 Role = j.Role,
		                AppliedDate = cp.AppliedDate,
		                Location = j.Location,
		                jobProcessId = j.Id,
		                status = cp.status
	                  }).ToList();
			return result;
        }
        public bool isJobApplied(int jobId, string email)
        {
            var candidate = db.candidates.FirstOrDefault(x => x.Email == email);
            if(candidate == null)
            {
                return false;
            }
            var result = db.candidatesProcess.FirstOrDefault(x => x.JobId == jobId && x.CandidateId == candidate.Id);
            if(result == null)
            {
                return false;
            }
            return true;
        }
        public List<InterviewListDTO> getListForSchedule()
        {
            List<InterviewListDTO> result = (from c in db.candidates
                          join cp in db.candidatesProcess on c.Id equals cp.CandidateId
                          join j in db.jobs on cp.JobId equals j.Id
                          where ((cp.status == "hr" && (cp.Hr==null|| cp.Hr == 0)) || (cp.status == "tr1" && cp.Tech1 == null) || (cp.status == "tr2" && (cp.Tech2 == null || cp.Tech2 == 0))) 
                          select new InterviewListDTO
                          {
                              CandidateId = c.Id,
                              Name = c.Name,
                              interviewType = cp.status == "tr1" ? "technical 1" : (cp.status == "tr2" ? "technical 2" : "General hr"),
                              jobId = cp.JobId,
                              jobName = j.Role

                          }).ToList();
            return result;
        }
        public CandidateDetails getIndividualDetails(int candidateId, int jobId)
        {
            CandidateDetails result = (from c in db.candidates
                                           join cp in db.candidatesProcess on c.Id equals cp.CandidateId
                                           join j in db.jobs on cp.JobId equals j.Id
                                           where c.Id == candidateId && cp.JobId == jobId
                                           select new CandidateDetails
                                           {
                                               Name = c.Name,
                                               Id = c.Id,
                                               JobId = cp.JobId,
                                               Experience = c.Experience,
                                               KeySkills = (c.KeySkills).Split(',', System.StringSplitOptions.None).ToList(),
                                               CurrentCTC = c.CurrentCTC,
                                               ExpectedCTC = c.ExpectedCTC,
                                               Email = c.Email,
                                               Status = cp.status,
                                               Notice = c.NoticePeriod,
                                               jobName = j.Role
                                           }).FirstOrDefault( );
            CandidateProcess candidateProcess = (from cp in db.candidatesProcess
                                                 where cp.CandidateId == candidateId && cp.JobId == jobId
                                                 select cp).FirstOrDefault();
            if (candidateProcess.Hr != null)
            {
                result.hrStatus = (from i in db.interviews
                                   where i.Id == candidateProcess.Hr
                                   select i.Status
                                   ).FirstOrDefault();

                result.tr1Status = (from i in db.interviews
                                   where i.Id == candidateProcess.Tech1
                                    select i.Status
                                   ).FirstOrDefault();
                result.tr2Status = (from i in db.interviews
                                   where i.Id == candidateProcess.Tech2
                                   select i.Status
                                   ).FirstOrDefault();

                result.hrId = candidateProcess.Hr;
                result.tr1Id = candidateProcess.Tech1;
                result.tr2Id = candidateProcess.Tech2;

            }
           else if(candidateProcess.Hr == null && candidateProcess.Tech1 == null && candidateProcess.Tech2 == null)
            {
                result.hrId = null;
                result.tr1Id = null;
                result.tr2Id = null;
                result.LastestStatus = null;
            }
            else if (candidateProcess.Hr == null  && candidateProcess.Tech2 == null)
            {
                result.hrId = null;
                result.tr1Id = candidateProcess.Tech1;
                Interview interview = db.interviews.FirstOrDefault(x => x.Id == candidateProcess.Tech1);
                if(interview != null) {
                    result.LastestStatus = interview.Status;
                }
                result.tr2Id = null;
            }
            else if (candidateProcess.Hr == null)
            {
                result.hrId = null;
                result.tr1Id = candidateProcess.Tech1;
                Interview interview = db.interviews.FirstOrDefault(x => x.Id == candidateProcess.Tech2);
                if (interview != null)
                {
                    result.LastestStatus = interview.Status;
                }
                result.tr2Id = candidateProcess.Tech2;
            }
            else 
            {
                result.hrId = candidateProcess.Hr;
                result.tr1Id = candidateProcess.Tech1;
                Interview interview = db.interviews.FirstOrDefault(x => x.Id == candidateProcess.Hr);
                if (interview != null)
                {
                    result.LastestStatus = interview.Status;
                }
                result.tr2Id = candidateProcess.Tech2;
            }
            return result;

        }
        public void approveApplication(int candidateId,int jobId)
        {
            CandidateProcess candidateProcess = (from cp in db.candidatesProcess
                                                 where cp.CandidateId == candidateId && cp.JobId == jobId
                                                 select cp).FirstOrDefault();
			Job job = (from j in db.jobs
					   where j.Id == jobId
					   select j).FirstOrDefault();
			job.OnProcess += 1;  
			candidateProcess.status = "tr1";
            db.SaveChanges();
        }
        public  async void declineApproval(int candidateId ,int  jobId)
        {
            CandidateProcess candidateProcess = (from cp in db.candidatesProcess
                                                 where cp.CandidateId == candidateId && cp.JobId == jobId
                                                 select cp).FirstOrDefault();
			
			candidateProcess.status = "not siuted";
            db.SaveChanges();
        }

        public List<NewFeedbackList> getNewFeedbackList()
        {
            List<NewFeedbackList> result = (from i in db.interviews
                                            join c in db.candidates on i.CandidateId equals c.Id
                                            join j in db.jobs on i.JobId equals j.Id
                                            where i.Status == "scheduled" && i.InterviewDate < DateTime.Now
                                            select new NewFeedbackList
                                            {
                                                CandidateId= i.CandidateId,
                                                CandidateName = c.Name,
                                                InterviewId = i.Id,
                                                InterviewType = i.typeOfRound,
                                                JobName = j.Role
                                            }).ToList();
            return result;
         }
        public SingleFeedbackDetails GetSingleFeedbackDetails(int id)
        {
            SingleFeedbackDetails result = (from f in db.feedbacks
                                            join i in db.interviews on f.interviewId equals i.Id
                                            join j in db.jobs on i.JobId equals j.Id
                                            join c in db.candidates on i.CandidateId equals c.Id
                                            where f.Id == id
                                            select new SingleFeedbackDetails
                                            {
                                                CandidateId = i.CandidateId,
                                                CandidateName = c.Name,
                                                oops = f.oopsRating,
                                                logicalThinking = f.logicalThinking,
                                                programming = f.programming,
                                                status = f.status,
                                                comments = f.comment,
                                                dateOnly = DateOnly.FromDateTime(i.InterviewDate),
                                                timeOnly = new TimeOnly(i.InterviewDate.Hour, i.InterviewDate.Minute),
                                                Role = j.Role,
                                                jobId = j.Id,
                                                InterviewPanel = i.InterviewPanel,
                                                TypeOfRound = i.typeOfRound
                                            }).FirstOrDefault();
            return result;
        }
	}
   
}
