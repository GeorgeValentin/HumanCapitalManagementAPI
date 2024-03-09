namespace HumanCapitalManagement.Service.Tests.ContractTests;

public class ContractServiceTests : TestBaseService
{
    private readonly IMapper mapper;
    private readonly Mock<IContractRepo> contractRepoMock;
    private readonly Mock<IEmployeeRepo> employeeRepoMock;
    private readonly Mock<IValidator<ContractCreateValidatorDto>> createContractValidatorMock;
    private readonly Mock<IValidator<ContractUpdateValidatorDto>> updateContractValidatorMock;
    private readonly Mock<IValidator<EmployeeExistanceValidatorDto>> employeeExistanceValidatorMock;
    private readonly Mock<IValidator<ContractExistanceValidatorDto>> contractExistanceValidatorMock;
    private ContractService sut;

    public ContractServiceTests()
    {
        mapper = MapperConfig<ContractProfile>.ConfigureMapper();
        contractRepoMock = new Mock<IContractRepo>();
        employeeRepoMock = new Mock<IEmployeeRepo>();
        createContractValidatorMock = new Mock<IValidator<ContractCreateValidatorDto>>();
        updateContractValidatorMock = new Mock<IValidator<ContractUpdateValidatorDto>>();
        employeeExistanceValidatorMock = new Mock<IValidator<EmployeeExistanceValidatorDto>>();
        contractExistanceValidatorMock = new Mock<IValidator<ContractExistanceValidatorDto>>();

        sut = new ContractService(
            contractRepoMock.Object,
            mapper,
            entitiesRepoMock.Object,
            employeeRepoMock.Object,
            createContractValidatorMock.Object,
            updateContractValidatorMock.Object,
            employeeExistanceValidatorMock.Object,
            contractExistanceValidatorMock.Object);
    }

    [Fact]
    public async void GetContract_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var contractResult = fixture.Build<Contract>()
            .With(a => a.Id, It.IsAny<int>())
            .Without(a => a.Employee)
            .Without(a => a.JobTitle)
            .Create();

        var expectedResult = mapper.Map<ContractDto>(contractResult);

        contractRepoMock
            .Setup(a => a.GetContract(It.IsAny<int>()).Result)
            .Returns(contractResult);

        // act
        var result = await sut.GetContract(It.IsAny<int>());

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void GetEmployeeContracts_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();
        var contractsCollectionResult = fixture.Build<Contract>()
            .With(a => a.Id, It.IsAny<int>())
            .Without(a => a.Employee)
            .Without(a => a.JobTitle)
            .CreateMany()
            .ToList();

        var expectedResult = mapper.Map<ICollection<ContractDto>>(contractsCollectionResult);

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        contractRepoMock
            .Setup(a => a.GetEmployeeContracts(It.IsAny<int>()).Result)
            .Returns(contractsCollectionResult);

        // act
        var result = await sut.GetEmployeeContracts(It.IsAny<int>());

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void AddContract_ReturnExpectedData_WhenNoContractDocumentExists()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
          .With(a => a.Id, It.IsAny<int>())
          .Create();

        var employeeContracts = fixture
            .CreateMany<Contract>(0)
            .ToList();

        var contractForCreation = fixture.Create<ContractForCreationDto>();
        var contractModel = mapper.Map<Contract>(contractForCreation);
        contractModel.InsertedDate = DateTimeOffset.UtcNow.Date;
        contractModel.InsertedUsername = $"{employeeResult!.FirstName} {employeeResult!.LastName}";

        var expectedResult = mapper.Map<ContractDto>(contractModel);

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        contractRepoMock
            .Setup(a => a.GetEmployeeContracts(It.IsAny<int>()).Result)
            .Returns(employeeContracts);

        contractRepoMock
            .Setup(a => a.AddContract(contractModel))
            .Returns(Task.CompletedTask);

        // act
        var result = await sut.AddContract(It.IsAny<int>(), contractForCreation);

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void AddContract_ReturnExpectedData_WhenContractDocumentsExistAndTheNewDateIsInvalid()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
          .With(a => a.Id, It.IsAny<int>())
          .Create();

        var contractForCreation = fixture.Create<ContractForCreationDto>();

        var employeeContracts = fixture
            .CreateMany<Contract>(3)
            .ToList();
        var sortedEmpContracts = employeeContracts
            .OrderBy(a => a.EndDate)
            .ToList();
        var latestDatedContract = sortedEmpContracts.Last();

        latestDatedContract.StartDate = new DateTime(2012, 02, 29, 12, 43, 0, DateTimeKind.Utc);
        contractForCreation.StartDate = latestDatedContract.StartDate.Date.AddYears(2); 

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        contractRepoMock
            .Setup(a => a.GetEmployeeContracts(It.IsAny<int>()).Result)
            .Returns(employeeContracts);

        // act
        var result = sut.AddContract(It.IsAny<int>(), contractForCreation);

        // assert
        Assert.NotNull(result);
        var exception = await Assert.ThrowsAsync<InvalidDateException>(async () => await result);
    }

    [Fact]
    public void AddContract_ReturnExpectedData_WhenContractDocumentsExistAndTheNewDateIsValid()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
          .With(a => a.Id, It.IsAny<int>())
          .Create();

        var contractForCreation = fixture.Create<ContractForCreationDto>();

        var employeeContracts = fixture
            .CreateMany<Contract>(3)
            .ToList();
        var sortedEmpContracts = employeeContracts
            .OrderBy(a => a.EndDate)
            .ToList();
        var latestDatedContract = sortedEmpContracts.Last();

        contractForCreation.StartDate = new DateTime(2012, 02, 29, 12, 43, 0, DateTimeKind.Utc);
        latestDatedContract.StartDate = latestDatedContract.StartDate.Date.AddYears(2);

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        contractRepoMock
            .Setup(a => a.GetEmployeeContracts(It.IsAny<int>()).Result)
            .Returns(employeeContracts);

        // act
        var result = sut.AddContract(It.IsAny<int>(), contractForCreation);

        // assert
        Assert.NotNull(result);
        Assert.True(result.IsCompleted);
    }

    [Fact]
    public void UpdateContract_ReturnExpectedData_WhenContractDocumentDoesNotExist()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
          .With(a => a.Id, It.IsAny<int>())
          .Create();

        var contractToUpdateDto = fixture.Build<ContractForUpdateDto>()
            .Without(a => a.StartDate)
            .Create();

        var contractToUpdate = mapper.Map<Contract>(contractToUpdateDto);

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        contractRepoMock
            .Setup(a => a.GetContract(It.IsAny<int>()).Result)
            .Returns(contractToUpdate);

        Task? expectedResult = null;
        contractRepoMock
            .Setup(a => a.UpdateContract(contractToUpdate))
            .Callback(() =>
            {
                expectedResult = Task.CompletedTask;
            });

        // act
        var result = sut.UpdateContract(It.IsAny<int>(), It.IsAny<int>(), contractToUpdateDto);

        // assert
        Assert.NotNull(result);
        Assert.True(expectedResult!.IsCompleted);
    }

    [Fact]
    public void UpdateContract_ReturnExpectedData_WhenContractDocumentExistsAndWhenNewStartDateIsValid()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
          .With(a => a.Id, It.IsAny<int>())
          .Create();

        var contractToUpdateDto = fixture.Create<ContractForUpdateDto>();

        var contractToUpdate = fixture.Build<Contract>()
            .With(a => a.Id, It.IsAny<int>())
            .With(a => a.JobTitleId, contractToUpdateDto.JobTitleId)
            .Create();

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        contractRepoMock
            .Setup(a => a.GetContract(It.IsAny<int>()).Result)
            .Returns(contractToUpdate);

        Task? expectedResult = null;
        contractRepoMock
            .Setup(a => a.UpdateContract(contractToUpdate))
            .Callback(() =>
            {
                expectedResult = Task.CompletedTask;
            });

        // act
        var result = sut.UpdateContract(It.IsAny<int>(), It.IsAny<int>(), contractToUpdateDto);

        // assert
        Assert.NotNull(result);
        Assert.True(expectedResult!.IsCompleted);
    }

    [Fact]
    public void UpdateContract_ReturnExpectedData_WhenNewLaterStartDateIsInvalidByBeingLaterThanLatestVariant()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
          .With(a => a.Id, It.IsAny<int>())
          .Create();

        var contractToUpdateDto = fixture.Create<ContractForUpdateDto>();

        var contractToUpdate = fixture.Build<Contract>()
            .With(a => a.Id, It.IsAny<int>())
            .With(a => a.JobTitleId, contractToUpdateDto.JobTitleId)
            .Create();

        var employeeContracts = fixture
            .CreateMany<Contract>(3)
            .ToList();
        var sortedEmpContracts = employeeContracts
            .OrderBy(a => a.EndDate)
            .ToList();
        var latestDatedContract = sortedEmpContracts.Last();

        contractToUpdate.StartDate = latestDatedContract.StartDate.Date.AddDays(20);

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        contractRepoMock
            .Setup(a => a.GetContract(It.IsAny<int>()).Result)
            .Returns(contractToUpdate);

        InvalidDateException? expectedResult = null;
        contractRepoMock
            .Setup(a => a.UpdateContract(contractToUpdate))
            .Callback(() =>
            {
                expectedResult = new InvalidDateException("Add a date that is earlier than the latest contract's!");
            });

        // act
        var result = sut.UpdateContract(It.IsAny<int>(), It.IsAny<int>(), contractToUpdateDto);

        // assert
        Assert.NotNull(result);
        var exception = Assert.ThrowsAsync<InvalidDateException>(() => result);
    }

    [Fact]
    public void UpdateContract_ReturnExpectedData_WhenNewLaterStartDateIsInvalidByBeingEarlierThanPresent()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
          .With(a => a.Id, It.IsAny<int>())
          .Create();

        var contractToUpdateDto = fixture.Create<ContractForUpdateDto>();

        var contractToUpdate = fixture.Build<Contract>()
            .With(a => a.Id, It.IsAny<int>())
            .With(a => a.JobTitleId, contractToUpdateDto.JobTitleId)
            .Create();

        var employeeContracts = fixture
            .CreateMany<Contract>(3)
            .ToList();
        var sortedEmpContracts = employeeContracts
            .OrderBy(a => a.EndDate)
            .ToList();
        var latestDatedContract = sortedEmpContracts.Last();

        contractToUpdate.StartDate = DateTimeOffset.UtcNow.Date.AddYears(-1);

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        contractRepoMock
            .Setup(a => a.GetContract(It.IsAny<int>()).Result)
            .Returns(contractToUpdate);

        InvalidDateException? expectedResult = null;
        contractRepoMock
            .Setup(a => a.UpdateContract(contractToUpdate))
            .Callback(() =>
            {
                expectedResult = new InvalidDateException("Add a date that is latter than the current date!");
            });

        // act
        var result = sut.UpdateContract(It.IsAny<int>(), It.IsAny<int>(), contractToUpdateDto);

        // assert
        Assert.NotNull(result);
        var exception = Assert.ThrowsAsync<InvalidDateException>(() => result);
    }
}
