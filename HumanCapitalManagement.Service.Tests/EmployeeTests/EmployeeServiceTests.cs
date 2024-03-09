namespace HumanCapitalManagement.Service.Tests.EmployeeTests;
public class EmployeeServiceTests : TestBaseService
{
    private readonly IMapper mapper;
    private readonly Mock<IEmployeeRepo> employeeRepoMock;
    private readonly Mock<IContractRepo> contractRepoMock;
    private readonly Mock<IAddressService> addressRepoMock;
    protected readonly Mock<IConfiguration> configurationMock;
    protected readonly Mock<IAzureServiceBus> azureServiceBus;
    private readonly Mock<IValidator<AddressForCreationValidatorDto>> createAddressValidator;
    private readonly Mock<IValidator<EmployeeForCreationValidatorDto>> createEmployeeValidator;
    private readonly Mock<IValidator<EmployeeForUpdateValidatorDto>> updateEmployeeValidator;
    private readonly Mock<IValidator<ContractExistanceValidatorDto>> contractExistanceValidator;
    private readonly Mock<IValidator<EmployeeExistanceValidatorDto>> employeeExistanceValidator;
    private EmployeesService sut;

    public EmployeeServiceTests()
    {
        mapper = MapperConfig<EmployeesProfile>.ConfigureMapper();
        employeeRepoMock = new Mock<IEmployeeRepo>();
        contractRepoMock = new Mock<IContractRepo>();
        addressRepoMock = new Mock<IAddressService>();
        configurationMock = new Mock<IConfiguration>();
        azureServiceBus = new Mock<IAzureServiceBus>();
        createAddressValidator = new Mock<IValidator<AddressForCreationValidatorDto>>();
        createEmployeeValidator = new Mock<IValidator<EmployeeForCreationValidatorDto>>();
        updateEmployeeValidator = new Mock<IValidator<EmployeeForUpdateValidatorDto>>();
        contractExistanceValidator = new Mock<IValidator<ContractExistanceValidatorDto>>();
        employeeExistanceValidator = new Mock<IValidator<EmployeeExistanceValidatorDto>>();

        AddSpecimenBuilder(new List<ISpecimenBuilder>()
        {
            new EmployeeForCreationDtoObjectSpecimenBuilder(),
            new EmployeeForUpdateDtoObjectSpecimenBuilder(),
            new AddressObjectSpecimenBuilder(),
            new EmployeeObjectSpecimenBuilder()
        });

        sut = new EmployeesService(
            employeeRepoMock.Object,
            contractRepoMock.Object,
            addressRepoMock.Object,
            mapper,
            entitiesRepoMock.Object,
            configurationMock.Object, 
            azureServiceBus.Object,
            createAddressValidator.Object,
            createEmployeeValidator.Object,
            updateEmployeeValidator.Object,
            contractExistanceValidator.Object,
            employeeExistanceValidator.Object);
    }

    [Fact]
    public async void GetEmployees_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var dbResult = fixture.CreateMany<Employee>();

        employeeRepoMock
            .Setup(a => a.GetEmployees().Result)
            .Returns(dbResult.ToList());

        var expectedResult = mapper.Map<ICollection<EmployeeDto>>(dbResult);

        // act
        var result = await sut.GetEmployees();

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void GetEmployee_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var dbResult = fixture.Create<Employee>();

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(dbResult);

        var expectedResult = mapper.Map<EmployeeDto>(dbResult);

        // act
        var result = await sut.GetEmployee(It.IsAny<int>());

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void CreateEmployee_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var validAddress = fixture.Create<Address>();

        var employeeForCreationDto = fixture.Create<EmployeeForCreationDto>();
        employeeForCreationDto.Address = validAddress;

        var dbInput = mapper.Map<Employee>(employeeForCreationDto);

        employeeRepoMock
            .Setup(a => a.AddEmployee(dbInput))
            .Returns(Task.CompletedTask);

        employeeRepoMock
            .Setup(a => a.GetEmployee(dbInput.Id).Result)
            .Returns(dbInput);

        var expectedResult = mapper.Map<EmployeeDto>(dbInput);

        // act
        var result = await sut.AddEmployee(employeeForCreationDto);

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void UpdateEmployee_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var validAddress = fixture.Create<Address>();

        var employeeForUpdateDto = fixture.Create<EmployeeForUpdateDto>();
        employeeForUpdateDto.Address = validAddress;

        var dbModel = mapper.Map<Employee>(employeeForUpdateDto);

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(dbModel);

        Task? expectResult = null;
        employeeRepoMock
            .Setup(a => a.UpdateEmployee(dbModel))
            .Callback(() => {
                expectResult = Task.CompletedTask;
            });

        // act
        await sut.UpdateEmployee(It.IsAny<int>(), employeeForUpdateDto);

        // assert
        Assert.True(expectResult?.IsCompleted);
    }

    [Fact]
    public async void DeleteEmployee_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var employeeDeleteParam = fixture.Create<JsonPatchDocument<EmployeeForCreationDto>>();

        var employeeContractsVariants = fixture.CreateMany<Contract>();
        contractRepoMock
            .Setup(a => a.GetEmployeeContracts(It.IsAny<int>()).Result)
            .Returns(employeeContractsVariants.ToList());

        var dbModel = fixture.Build<Employee>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();
        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(dbModel);

        Task? expectedResult = null;
        employeeRepoMock
            .Setup(a => a.DeleteEmployee(dbModel, employeeDeleteParam))
            .Callback(() => expectedResult = Task.CompletedTask);

        var latestContractVariant = employeeContractsVariants.Last();
        latestContractVariant.EndDate = null;
        DateTimeOffset expectedEndDate = DateTimeOffset.UtcNow;
        contractRepoMock
            .Setup(a => a.UpdateContract(latestContractVariant))
            .Callback(() => expectedEndDate = latestContractVariant.StartDate.Date.AddDays(20));

        // act
        await sut.DeleteEmployee(dbModel.Id, employeeDeleteParam);

        // assert
        Assert.Equal(latestContractVariant.StartDate.Date.AddDays(20), expectedEndDate.Date);
        Assert.True(dbModel.IsDeleted);
        Assert.True(expectedResult?.IsCompleted);
    }
}
