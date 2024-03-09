using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Persistance.Repositories
{
    public interface IFeebackRepo
    {
        Task<Feedback?> GetFeedbackById(int feedbackId);
        Task<ICollection<Feedback>?> GetFeedbacksByReviewerId(int employeeId);
        Task<ICollection<Feedback>?> GetFeedbacksByRevieweeId(int employeeId);
        Task AddFeedback(Feedback feedback);
    }
}
