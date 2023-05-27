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
		            Role= j.Role,
		            Location = j.Location,
		            Type =j.Type,
		            Vaccanies =j.Vaccanies,
		            ExperienceRequired =j.ExperienceRequired,
		            Description = j.Description,
	            });
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
                           select new CandidateProfileData {
                               Name = c.Name,
                               Id = c.Id,
                               Experience = c.Experience,
                               KeySkills = c.KeySkills.Split(new char[] { ',' }).ToList(),
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
            CandidateProcess candidateProcess = await db.candidatesProcess.FirstOrDefaultAsync(x => x.JobId == candidate.JobId && profileData.Id == x.CandidateId);
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
            await db.candidates.AddAsync(profileData);
            CandidateProcess candidateProcess1 = new CandidateProcess();
            candidateProcess1.CandidateId = profileData.Id;
            candidateProcess1.JobId = candidate.JobId;
            candidateProcess1.AppliedDate = DateTime.Today;
            candidateProcess1.status = "applied";
            await db.candidatesProcess.AddAsync(candidateProcess1);
            await db.SaveChangesAsync();

            formResponse.result = "success";
            formResponse.Message = "Suucessfully applied  for the job";
			return formResponse;

		}
		public async Task<FormResponse> scheduleInterview(ScheduleInterview scheduleInterview)
		{
            FormResponse formResponse = new FormResponse();
			Interview interview = new Interview();
			interview.InterviewDate = scheduleInterview.date_only.ToString();
			interview.InterviewTime = scheduleInterview.TimeOnly.ToString();
			interview.InterviewPanel = scheduleInterview.InterviewPanel;
			interview.CandidateId = scheduleInterview.candidateId;
			interview.JobId = scheduleInterview.jobId;
			interview.typeOfRound = scheduleInterview.type_of_round;
			await db.interviews.AddAsync(interview);
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
            await db.feedbacks.AddAsync(feedback);
            await db.AddRangeAsync();
			formResponse.result = "success";
			return formResponse;

        }
        

	}
   
}
