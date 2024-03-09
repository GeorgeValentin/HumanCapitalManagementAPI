using AutoMapper;
using FluentValidation;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.FeedbackDTOs;
using HumanCapitalManagement.Persistance.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HumanCapitalManagement.Service.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IEntitiesRepo _entitiesRepo;
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IFeebackRepo _feedbackRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<EmployeeExistanceValidatorDto> _employeeExistanceValidator;
        private readonly IValidator<FeedbackForCreationValidatorDto> _createNewFeedbackValidator;

        public FeedbackService(
            IEntitiesRepo entitiesRepo, 
            IEmployeeRepo employeeRepo, 
            IFeebackRepo feebackRepo, 
            IMapper mapper,
            IValidator<EmployeeExistanceValidatorDto> employeeExistanceValidator,
            IValidator<FeedbackForCreationValidatorDto> createNewFeedbackValidator)
        {
            _entitiesRepo = entitiesRepo ?? throw new ArgumentNullException(nameof(entitiesRepo));
            _employeeRepo = employeeRepo ?? throw new ArgumentNullException(nameof(employeeRepo));
            _feedbackRepo = feebackRepo ?? throw new ArgumentNullException(nameof(feebackRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _employeeExistanceValidator = employeeExistanceValidator ?? throw new ArgumentNullException(nameof(employeeExistanceValidator));
            _createNewFeedbackValidator = createNewFeedbackValidator ?? throw new ArgumentNullException(nameof(createNewFeedbackValidator));
        }

        public async Task<FeedbackDto> AddFeedback([FromBody]FeedbackForCreationDto feedbackForCreationDto)
        {
            Employee? fromEmployee = await _employeeRepo.GetEmployee(feedbackForCreationDto.FromEmployeeId);
            await _employeeExistanceValidator.ValidateAndThrowAsync(
                new EmployeeExistanceValidatorDto { Employee = fromEmployee, EmployeeId = feedbackForCreationDto.FromEmployeeId });

            Employee? toEmployee = await _employeeRepo.GetEmployee(feedbackForCreationDto.ToEmployeeId);
            await _employeeExistanceValidator.ValidateAndThrowAsync(
                new EmployeeExistanceValidatorDto { Employee = toEmployee, EmployeeId = feedbackForCreationDto.ToEmployeeId });

            await _createNewFeedbackValidator.ValidateAndThrowAsync(_mapper.Map<FeedbackForCreationValidatorDto>(feedbackForCreationDto));
            Feedback feedback = _mapper.Map<Feedback>(feedbackForCreationDto);

            feedback.InsertedUsername = $"{fromEmployee!.FirstName} {fromEmployee.LastName}";
            feedback.InsertedDate = DateTimeOffset.UtcNow;

            await _feedbackRepo.AddFeedback(feedback);
            await _entitiesRepo.SaveChanges();

            Feedback? addedFeedback = await _feedbackRepo.GetFeedbackById(feedback.Id);
            FeedbackDto feedbackToReturn = _mapper.Map<FeedbackDto>(addedFeedback);

            return feedbackToReturn;
        }

        public async Task<FeedbackDto?> GetFeedbackById(int feedbackId)
        {
            Feedback? feedback = await _feedbackRepo.GetFeedbackById(feedbackId);
            FeedbackDto feedbackDto = _mapper.Map<FeedbackDto>(feedback);

            return feedbackDto;
        }

        public async Task<ICollection<FeedbackDto>> GetFeedbacks(int employeeId, AssessorType assessorType)
        {
            Employee? employee = await _employeeRepo.GetEmployee(employeeId);
            await _employeeExistanceValidator.ValidateAndThrowAsync(
                new EmployeeExistanceValidatorDto { Employee = employee, EmployeeId = employeeId });

            ICollection<Feedback>? feedbacks = null;

            switch (assessorType)
            {
                case AssessorType.Reviewer:
                    feedbacks = await _feedbackRepo.GetFeedbacksByReviewerId(employeeId);
                    break;
                case AssessorType.Reviewee:
                    feedbacks = await _feedbackRepo.GetFeedbacksByRevieweeId(employeeId);
                    break;
                case AssessorType.Unknown:
                    throw new NotSupportedException("The assesor you specified is unknown!");
                default: 
                    throw new ArgumentException("The assesor you specified does not exist!");
            }

            ICollection<FeedbackDto> feedbackDto = _mapper.Map<ICollection<FeedbackDto>>(feedbacks);

            return feedbackDto;
        }
    }
}
