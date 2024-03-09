using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace HumanCapitalManagement.Persistance.Repositories
{
    public class FeedbackRepo : IFeebackRepo
    {
        private readonly ApplicationDbContext _context;

        public FeedbackRepo(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Feedback?> GetFeedbackById(int feedbackId)
        {
            Feedback? feedback = await _context.Feedbacks
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == feedbackId);

            Log.Information("[{class}.{method}] has been called, retrieving the feedback: {feedback}.",
                this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), JsonConvert.SerializeObject(feedback));

            return feedback;
        }

        public async Task<ICollection<Feedback>?> GetFeedbacksByRevieweeId(int employeeId)
        {
            ICollection<Feedback>? feedbacks = await _context.Feedbacks
                .AsNoTracking()
                .Where(a => a.ToEmployeeId == employeeId)
                .ToListAsync();

            Log.Information("[{class}.{method}] has been called, retrieving the {feedbackCount} feedbacks of the employee.",
                this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), feedbacks.Count, employeeId);

            return feedbacks;
        }

        public async Task<ICollection<Feedback>?> GetFeedbacksByReviewerId(int employeeId)
        {
            ICollection<Feedback> feedbacks = await _context.Feedbacks
                .AsNoTracking()
                .Where(a => a.FromEmployeeId == employeeId)
                .ToListAsync();

            Log.Information("[{class}.{method}] has been called, retrieving the {feedbackCount} feedbacks of the employee.",
                this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), feedbacks.Count, employeeId);

            return feedbacks;
        }

        public async Task AddFeedback(Feedback feedback)
        {
            Log.Information("[{class}.{method}] has been called, adding a feedback to the context.",
                this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

            await _context.Feedbacks.AddAsync(feedback);
        }
    }
}
