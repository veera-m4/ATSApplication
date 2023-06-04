using byteforzaFinalProject.DTO;
using byteforzaFinalProject.Models;
using byteforzaFinalProject.Response;
using Microsoft.AspNetCore.Mvc;

namespace byteforzaFinalProject.Interface
{
    public interface CandidateInterface
    {
        public List<CandidateProfileData> GetCandidatePageAsync();
		public Task<CandidatePageResponse> EditCandidateDetailsAsync(Candidate candidate);
        public Task<CandidatePageResponse> DeleteCandidateDetailsAsync(int candidateId);
        public Task <FormResponse> SaveCandidateDetailsAsync(NewCandidateFormDTO candidate);
        public bool IsEmailAlreadyExists(string email);
        public Task<FormResponse> AddJobAsynac(NewJobDTO newJobDTO);
        public Task<FormResponse> EditJobDetails(NewJobDTO newJob);
        public Task<FormResponse> DeleteJobDetails(int i);
        public Task<List<JobDTO>> jobDetails();
        public Task<Job> getJobDetails(int i);
        public Task<FormResponse> scheduleInterview(ScheduleInterview scheduleInterview);
        public Task<FormResponse> AddFeedBack(AddFeedback addFeedback);
        public List<FeedbackPageDTO> getFeedBackPageDetails();
        public List<Object> graphData();
        public List<InterviewScheludeDetail> getTheScheduledInterview();
        public List<InterviewReportDetails> getInterviewReport();
        public ReportPageDetails GetReportPageDetails();
        public List<AppliedJobDTO> CandidateAppliedJob(string email);
        public bool isJobApplied(int jobId, string email);
        public Task<FormResponse> AddCandidateProcess(int jobId, string email);
        public Task<FormResponse> addProfile(ProfileDTO candidate);
        public List<InterviewListDTO> getListForSchedule();
        public CandidateDetails getIndividualDetails(int candidateId, int jobId);
        public void approveApplication(int candidateId, int jobId);
        public void declineApproval(int candidateId, int jobId);
        public List<NewFeedbackList> getNewFeedbackList();
        public SingleFeedbackDetails GetSingleFeedbackDetails(int id);


    }
}
