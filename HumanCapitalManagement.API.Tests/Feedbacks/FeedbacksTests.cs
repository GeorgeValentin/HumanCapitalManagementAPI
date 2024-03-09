namespace HumanCapitalManagement.API.Tests.Feedbacks
{
    public class FeedbacksTests
    {
        private readonly Fixture fixture;
        private readonly Mock<IFeedbackService> feedbackServiceMock;
        private readonly FeedbacksController sut;

        public FeedbacksTests()
        {
            fixture = new Fixture();
            feedbackServiceMock = new Mock<IFeedbackService>();
            sut = new FeedbacksController(feedbackServiceMock.Object);
        }

        [Fact]
        public async void GetFeedback_ReturnExpectedData_WhenRequestIsValid()
        {
            // arrange
            var expectedFeedback = fixture.Create<FeedbackDto>();

            feedbackServiceMock.Setup(s => s.GetFeedbackById(It.IsAny<int>()).Result)
                .Returns(expectedFeedback);

            // act
            var actionResponse = await sut.GetFeedback(It.IsAny<int>());
            var response = (OkObjectResult)actionResponse.Result!;

            // assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        }

        [Fact]
        public async void GetFeedback_ReturnExpectedData_WhenRequestIsInvalid()
        {
            // arrange
            feedbackServiceMock.Setup(s => s.GetFeedbackById(It.IsAny<int>()).Result)
                .Returns((FeedbackDto?)null);

            // act
            var actionResponse = await sut.GetFeedback(It.IsAny<int>());
            var response = (NotFoundResult)actionResponse.Result!;

            // assert
            Assert.IsType<NotFoundResult>(response);
            Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [Fact]
        public async void GetFeedbacks_ReturnExpectedData_WhenRequestIsValid()
        {
            // arrange
            var collectionExpected = fixture.CreateMany<FeedbackDto>(3);

            feedbackServiceMock.Setup(a => a.GetFeedbacks(It.IsAny<int>(), It.IsAny<AssessorType>()).Result)
                .Returns(collectionExpected.ToList());

            // act
            var actionResponse = await sut.GetFeedbacks(It.IsAny<int>(), It.IsAny<AssessorType>());
            var response = (OkObjectResult)actionResponse.Result!;

            // assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        }

        [Fact]
        public async void CreateFeedback_ReturnExpectedResponse_WhenRequestIsValid()
        {
            // arrange
            var feedbackDtoInput = fixture.Create<FeedbackForCreationDto>();
            var feedbackDtoOutput = fixture.Create<FeedbackDto>();

            feedbackServiceMock.Setup(s => s.AddFeedback(feedbackDtoInput).Result)
                .Returns(feedbackDtoOutput);

            // act
            var actionResponse = await sut.CreateFeedback(feedbackDtoInput);
            var response = (CreatedAtRouteResult)actionResponse.Result!;

            // assert
            Assert.IsType<CreatedAtRouteResult>(response);
            Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
        }
    }
}