using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.FeedbackDTOs;

namespace HumanCapitalManagement.Service.Services
{
    public interface IFeedbackService
    {
        Task<FeedbackDto?> GetFeedbackById(int feedbackId);
        Task<ICollection<FeedbackDto>> GetFeedbacks(int employeeId, AssessorType assessorType);
        Task<FeedbackDto> AddFeedback(FeedbackForCreationDto feedbackForCreationDto);
    }
}
