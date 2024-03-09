using FluentAssertions;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.FeedbackDTOs;
using HumanCapitalManagement.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanCapitalManagement.Service.Tests.FeedbackTests
{
    public class FeedbackServiceTests : TestBaseService
    {
        private readonly IMapper mapper;
        private readonly Mock<IFeebackRepo> feedbackRepoMock;
        private readonly Mock<IEmployeeRepo> employeeRepoMock;
        private readonly Mock<IValidator<FeedbackForCreationValidatorDto>> createFeedbackValidatorMock;
        private readonly Mock<IValidator<EmployeeExistanceValidatorDto>> employeeExistanceValidatorMock;
        private FeedbackService sut;

        public FeedbackServiceTests()
        {
            mapper = MapperConfig<FeedbackProfile>.ConfigureMapper();
            feedbackRepoMock = new Mock<IFeebackRepo>();
            employeeRepoMock = new Mock<IEmployeeRepo>();
            createFeedbackValidatorMock = new Mock<IValidator<FeedbackForCreationValidatorDto>>();
            employeeExistanceValidatorMock = new Mock<IValidator<EmployeeExistanceValidatorDto>>();

            sut = new FeedbackService(
            entitiesRepoMock.Object,
            employeeRepoMock.Object,
            feedbackRepoMock.Object,
            mapper,
            employeeExistanceValidatorMock.Object,
            createFeedbackValidatorMock.Object);
        }

        [Fact]
        public async void GetFeedbackById_ReturnExpectedData_WhenDataExists()
        {
            // arrange
            var dbResult = fixture.Build<Feedback>()
                .With(a => a.Id, It.IsAny<int>())
                .Without(a => a.FromEmployee)
                .Without(a => a.ToEmployee)
                .Create();

            feedbackRepoMock
                .Setup(a => a.GetFeedbackById(It.IsAny<int>()).Result)
                .Returns(dbResult);

            var expectedResponseDto = mapper.Map<FeedbackDto>(dbResult);

            // act
            var result = await sut.GetFeedbackById(It.IsAny<int>());

            // assert
            Assert.NotNull(result);
            expectedResponseDto.Should().BeEquivalentTo(result);
        }

        [Theory]
        [InlineData(AssessorType.Reviewer, 1, 0)]
        [InlineData(AssessorType.Reviewee, 0, 1)]
        public async void GetFeedbacks_ReturnExpectedData_WhenDataExists(AssessorType assessorType, int calledByReviewer, int calledByReviewee)
        {
            // arrange
            var dbResult = fixture.Build<Feedback>()
                .With(a => a.Id, It.IsAny<int>())
                .Without(a => a.FromEmployee)
                .Without(a => a.ToEmployee)
                .CreateMany(3);

            feedbackRepoMock
                .Setup(a => a.GetFeedbacksByReviewerId(It.IsAny<int>()).Result)
                .Returns(dbResult.ToList());

            feedbackRepoMock
                .Setup(a => a.GetFeedbacksByRevieweeId(It.IsAny<int>()).Result)
                .Returns(dbResult.ToList());

            var expectedResponseDtoReviewer = mapper.Map< ICollection<FeedbackDto>>(dbResult);
            var expectedResponseDtoReviewee = mapper.Map<ICollection<FeedbackDto>>(dbResult);

            // act
            var result = await sut.GetFeedbacks(It.IsAny<int>(), assessorType);

            // assert
            Assert.NotNull(result);
            feedbackRepoMock.Verify(a => a.GetFeedbacksByReviewerId(It.IsAny<int>()), Times.Exactly(calledByReviewer));
            feedbackRepoMock.Verify(a => a.GetFeedbacksByRevieweeId(It.IsAny<int>()), Times.Exactly(calledByReviewee));
            dbResult.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async void GetFeedbacks_ReturnExpectedData_WhenDataExists_WhenAssesorTypeIsUnknown()
        {
            // arrange
            var dbResult = fixture.Build<Feedback>()
                .With(a => a.Id, It.IsAny<int>())
                .Without(a => a.FromEmployee)
                .Without(a => a.ToEmployee)
                .CreateMany(3);

            feedbackRepoMock
                .Setup(a => a.GetFeedbacksByReviewerId(It.IsAny<int>()).Result)
                .Returns(dbResult.ToList());

            feedbackRepoMock
                .Setup(a => a.GetFeedbacksByRevieweeId(It.IsAny<int>()).Result)
                .Returns(dbResult.ToList());

            var expectedResponseDto = mapper.Map<ICollection<FeedbackDto>>(dbResult);

            // act and assert
            NotSupportedException exception = await Assert.ThrowsAsync<NotSupportedException>(() => sut.GetFeedbacks(It.IsAny<int>(), AssessorType.Unknown));

            Assert.Equal("The assesor you specified is unknown!", exception.Message);
            feedbackRepoMock.Verify(a => a.GetFeedbacksByReviewerId(It.IsAny<int>()), Times.Never());
            feedbackRepoMock.Verify(a => a.GetFeedbacksByRevieweeId(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public async void AddFeedback_ReturnExpectedData_WhenDataExists()
        {
            // arrange
            var creationDto = fixture.Build<FeedbackForCreationDto>()
                .Create();

            var fromEmployee = fixture.Build<Employee>()
                .With(a => a.Id, It.IsAny<int>())
                .Without(a => a.EmployeeStudyPrograms)
                .Without(a => a.Skills)
                .Without(a => a.Contracts)
                .Create();

            var toEmployee = fixture.Build<Employee>()
                .With(a => a.Id, It.IsAny<int>())
                .Without(a => a.EmployeeStudyPrograms)
                .Without(a => a.Skills)
                .Without(a => a.Contracts)
                .Create();

            var dbResult = fixture.Build<Feedback>()
                .With(a => a.Id, It.IsAny<int>())
                .With(a => a.FromEmployee, fromEmployee)
                .With(a => a.FromEmployeeId, It.IsAny<int>())
                .With(a => a.ToEmployee, toEmployee)
                .With(a => a.ToEmployeeId, It.IsAny<int>())
                .Create();

            employeeRepoMock
                .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
                .Returns(fromEmployee);

            employeeRepoMock
                .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
                .Returns(toEmployee);

            feedbackRepoMock
                .Setup(a => a.AddFeedback(dbResult))
                .Returns(Task.CompletedTask);

            var feedback = mapper.Map<Feedback>(creationDto);

            feedbackRepoMock
                .Setup(a => a.GetFeedbackById(It.IsAny<int>()).Result)
                .Returns(feedback);

            var expectedResponseDto = mapper.Map<FeedbackDto>(creationDto);

            // act
            var result = await sut.AddFeedback(creationDto);

            // assert
            Assert.NotNull(result);
            expectedResponseDto.Should().BeEquivalentTo(result);
        }
    }
}

