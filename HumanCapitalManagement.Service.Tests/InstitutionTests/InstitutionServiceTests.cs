namespace HumanCapitalManagement.Service.Tests.InstitutionTests;

public class InstitutionServiceTests : TestBaseService
{
    private readonly Mock<IInstitutionRepo> institutionRepoMock;
    private readonly IMapper mapper;
    private readonly Mock<IValidator<CreateInstitutionValidatorDto>> createInstitutionValidatorMock;
    private readonly Mock<IValidator<CreateFacultyValidatorDto>> createFacultyValidatorMock;
    private readonly Mock<IValidator<CreateStudyProgramValidatorDto>> createStudyProgramValidatorMock;
    private readonly Mock<IValidator<InstitutionExistanceValidatorDto>> institutionExistanceValidatorMock;
    private readonly Mock<IValidator<FacultyExistanceValidatorDto>> facultyExistanceValidatorMock;
    private InstitutionService sut;

    public InstitutionServiceTests()
    {
        mapper = MapperConfig<InstitutionProfile>.ConfigureMapper();
        institutionRepoMock = new Mock<IInstitutionRepo>();
        createInstitutionValidatorMock = new Mock<IValidator<CreateInstitutionValidatorDto>>();
        createFacultyValidatorMock = new Mock<IValidator<CreateFacultyValidatorDto>>();
        createStudyProgramValidatorMock = new Mock<IValidator<CreateStudyProgramValidatorDto>>();
        institutionExistanceValidatorMock = new Mock<IValidator<InstitutionExistanceValidatorDto>>();
        facultyExistanceValidatorMock = new Mock<IValidator<FacultyExistanceValidatorDto>>();

        sut = new InstitutionService(
            entitiesRepoMock.Object,
            institutionRepoMock.Object,
            mapper,
            createInstitutionValidatorMock.Object,
            createFacultyValidatorMock.Object,
            createStudyProgramValidatorMock.Object,
            institutionExistanceValidatorMock.Object,
            facultyExistanceValidatorMock.Object);
    }

    [Fact]
    public async void GetInstitutions_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var dbResult = fixture.Build<Institution>()
            .With(a => a.Id, It.IsAny<int>())
            .CreateMany(3);

        institutionRepoMock
            .Setup(a => a.GetInstitutions().Result)
            .Returns(dbResult.ToList());

        var expectedResponseDto = mapper.Map<ICollection<InstitutionDto>>(dbResult);

        // act
        var result = await sut.GetInstitutions();

        // assert
        Assert.NotNull(result);
        expectedResponseDto.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void GetInstitution_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var dbResult = fixture.Build<Institution>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();

        institutionRepoMock
            .Setup(a => a.GetInstitution(It.IsAny<int>()).Result)
            .Returns(dbResult);

        var expectedResponseDto = mapper.Map<InstitutionDto>(dbResult);

        // act
        var result = await sut.GetInstitution(It.IsAny<int>());

        // assert
        Assert.NotNull(result);
        expectedResponseDto.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void GetFaculty_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var dbResultInstitution = fixture.Build<Institution>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();

        institutionRepoMock
            .Setup(a => a.GetInstitution(It.IsAny<int>()).Result)
            .Returns(dbResultInstitution);

        var dbResultFaculty = fixture.Build<Faculty>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();

        institutionRepoMock
            .Setup(a => a.GetFaculty(It.IsAny<int>(), dbResultInstitution).Result)
            .Returns(dbResultFaculty);

        var expectedResponseDto = mapper.Map<FacultyDto>(dbResultFaculty);

        // act
        var result = await sut.GetFaculty(It.IsAny<int>(), It.IsAny<int>());

        // assert
        Assert.NotNull(result);
        expectedResponseDto.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void GetFaculties_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var dbResultInstitution = fixture.Build<Institution>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();

        institutionRepoMock
            .Setup(a => a.GetInstitution(It.IsAny<int>()).Result)
            .Returns(dbResultInstitution);

        var dbResultFaculty = fixture.Build<Faculty>()
            .With(a => a.Id, It.IsAny<int>())
            .CreateMany(3);

        institutionRepoMock
            .Setup(a => a.GetFaculties(dbResultInstitution).Result)
            .Returns(dbResultFaculty.ToList());

        var expectedResponseDto = mapper.Map<ICollection<FacultyDto>>(dbResultFaculty);

        // act
        var result = await sut.GetFaculties(It.IsAny<int>());

        // assert
        Assert.NotNull(result);
        expectedResponseDto.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void GetStudyPrograms_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var employeeId = 1;
        var institutionId = 1;
        var facultyId = 1;
        var studyProgramId = 1;

        var employeeStudyPrograms = fixture
            .Build<Domain.Models.EmployeeStudyProgram>()
            .With(a => a.EmployeeId, employeeId)
            .Without(a => a.Employee)
            .With(a => a.StudyProgramId, studyProgramId)
            .Without(a => a.StudyProgram)
            .CreateMany<Domain.Models.EmployeeStudyProgram>();

        fixture.Inject(employeeStudyPrograms);

        var dbResultInstitution = fixture.Build<Institution>()
            .With(a => a.Id, institutionId)
            .Create();

        institutionRepoMock
            .Setup(a => a.GetInstitution(institutionId).Result)
            .Returns(dbResultInstitution);

        var dbResultFaculty = fixture.Build<Faculty>()
            .With(a => a.Id, facultyId)
            .Create();

        institutionRepoMock
            .Setup(a => a.GetFaculty(facultyId, dbResultInstitution).Result)
            .Returns(dbResultFaculty);

        var dbResultStudyProgram = fixture.Build<StudyProgram>()
            .With(a => a.Id, studyProgramId)
            .With(a => a.Employees, employeeStudyPrograms.ToList())
            .With(a => a.FacultyId, dbResultFaculty.Id)
            .CreateMany(3);

        institutionRepoMock
            .Setup(a => a.GetStudyPrograms(dbResultInstitution, dbResultFaculty).Result)
            .Returns(dbResultStudyProgram.ToList());

        var expectedResponseDto = mapper.Map<ICollection<StudyProgramDto>>(dbResultStudyProgram);

        // act
        var result = await sut.GetStudyPrograms(institutionId, facultyId);

        // assert
        Assert.NotNull(result);
        expectedResponseDto.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void GetStudyProgram_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var employeeId = 1;
        var institutionId = 1;
        var facultyId = 1;
        var studyProgramId = 1;

        var employeeStudyPrograms = fixture
            .Build<Domain.Models.EmployeeStudyProgram>()
            .With(a => a.EmployeeId, employeeId)
            .Without(a => a.Employee)
            .With(a => a.StudyProgramId, studyProgramId)
            .Without(a => a.StudyProgram)
            .CreateMany<Domain.Models.EmployeeStudyProgram>();

        fixture.Inject(employeeStudyPrograms);

        var dbResultInstitution = fixture.Build<Institution>()
            .With(a => a.Id, institutionId)
            .Create();

        institutionRepoMock
            .Setup(a => a.GetInstitution(institutionId).Result)
            .Returns(dbResultInstitution);

        var dbResultFaculty = fixture.Build<Faculty>()
            .With(a => a.Id, facultyId)
            .Create();

        institutionRepoMock
            .Setup(a => a.GetFaculty(facultyId, dbResultInstitution).Result)
            .Returns(dbResultFaculty);

        var dbResultStudyProgram = fixture.Build<StudyProgram>()
            .With(a => a.Id, studyProgramId)
            .With(a => a.Employees, employeeStudyPrograms.ToList())
            .With(a => a.FacultyId, dbResultFaculty.Id)
            .Create();

        institutionRepoMock
            .Setup(a => a.GetStudyProgram(studyProgramId, dbResultInstitution, dbResultFaculty).Result)
            .Returns(dbResultStudyProgram);

        var expectedResponseDto = mapper.Map<StudyProgramDto>(dbResultStudyProgram);

        // act
        var result = await sut.GetStudyProgram(institutionId, facultyId, studyProgramId);

        // assert
        Assert.NotNull(result);
        expectedResponseDto.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void AddInstitution_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var creationDto = fixture.Build<InstitutionForCreationDto>()
            .Create();

        var dbResult = fixture.Build<Institution>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();

        institutionRepoMock
            .Setup(a => a.AddInstitution(dbResult))
            .Returns(Task.CompletedTask);

        var expectedResponseDto = mapper.Map<InstitutionDto>(creationDto);

        // act
        var result = await sut.AddInstitution(creationDto);

        // assert
        Assert.NotNull(result);
        expectedResponseDto.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void AddFaculty_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var creationDto = fixture.Build<FacultyForCreationDto>()
            .Create();

        var dbResult = fixture.Build<Institution>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();

        var dbResultFaculty = fixture.Build<Faculty>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();

        institutionRepoMock
            .Setup(a => a.GetInstitution(It.IsAny<int>()).Result)
            .Returns(dbResult);

        var faculty = mapper.Map<Faculty>(creationDto);

        institutionRepoMock
            .Setup(a => a.AddFaculty(faculty))
            .Returns(Task.CompletedTask);

        var expectedResponseDto = mapper.Map<FacultyDto>(creationDto);

        institutionRepoMock
            .Setup(a => a.GetFaculty(It.IsAny<int>(), dbResult).Result)
            .Returns(faculty);

        // act
        var result = await sut.AddFaculty(creationDto, It.IsAny<int>());

        // assert
        Assert.NotNull(result);
        expectedResponseDto.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void AddStudyProgram_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var employeeStudyPrograms = fixture
            .Build<Domain.Models.EmployeeStudyProgram>()
            .With(a => a.EmployeeId, 1)
            .Without(a => a.Employee)
            .With(a => a.StudyProgramId, 1)
            .Without(a => a.StudyProgram)
            .CreateMany<Domain.Models.EmployeeStudyProgram>();

        var creationDto = fixture.Build<StudyProgramForCreationDto>()
            .Create();

        var dbResultInstitution = fixture.Build<Institution>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();

        var dbResultFaculty = fixture.Build<Faculty>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();

        var dbResultStudyProgram = fixture.Build<StudyProgram>()
            .With(a => a.Id, 1)
            .With(a => a.Employees, employeeStudyPrograms.ToList())
            .With(a => a.FacultyId, dbResultFaculty.Id)
            .Create();

        institutionRepoMock
            .Setup(a => a.GetInstitution(It.IsAny<int>()).Result)
            .Returns(dbResultInstitution);

        institutionRepoMock
            .Setup(a => a.GetFaculty(It.IsAny<int>(), dbResultInstitution).Result)
            .Returns(dbResultFaculty);

        var studyProgram = mapper.Map<StudyProgram>(creationDto);

        institutionRepoMock
            .Setup(a => a.AddStudyProgram(studyProgram))
            .Returns(Task.CompletedTask);

        var expectedResponseDto = mapper.Map<StudyProgramDto>(creationDto);

        institutionRepoMock
            .Setup(a => a.GetStudyProgram(It.IsAny<int>(), dbResultInstitution, dbResultFaculty).Result)
            .Returns(studyProgram);

        // act
        var result = await sut.AddStudyProgram(creationDto, It.IsAny<int>(), It.IsAny<int>());

        // assert
        Assert.NotNull(result);
        expectedResponseDto.Should().BeEquivalentTo(result);
    }
}

