using AutoMapper;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.FeedbackDTOs;

namespace HumanCapitalManagement.Entities.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<Feedback, FeedbackDto>().ReverseMap();
            CreateMap<Feedback, FeedbackForCreationDto>().ReverseMap();
            CreateMap<FeedbackForCreationValidatorDto, FeedbackForCreationDto>().ReverseMap();
            CreateMap<FeedbackDto, FeedbackForCreationDto>().ReverseMap();
        }
    }
}
