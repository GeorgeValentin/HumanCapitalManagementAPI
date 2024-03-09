namespace HumanCapitalManagement.Persistance.Tests.InstitutionTests
{
    public class InstitutionRepoTests : TestBasePersistance
    {
        private InstitutionRepo sut;

        public InstitutionRepoTests()
        {
            sut = new InstitutionRepo(context);
        }

        [Fact]
        public async Task GetInstitution_ReturnExpectedData_WhenDataExists()
        {
            // act
            var result = await sut.GetInstitution(1);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetInstitutions_ReturnExpectedData_WhenDataExists()
        {
            // act
            var result = await sut.GetInstitutions();

            // assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetFaculty_ReturnExpectedData_WhenDataExists()
        {
            // act
            var institution = await sut.GetInstitution(1);
            var faculty = await sut.GetFaculty(1, institution!);

            // assert
            Assert.NotNull(institution);
        }

        [Fact]
        public async Task GetFaculties_ReturnExpectedData_WhenDataExists()
        {
            // arrange
            var institution = await context.Institutions
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == 1);

            // act
            var faculty = await sut.GetFaculties(institution!);

            // assert
            Assert.Equal(3, faculty.Count);
        }

        [Fact]
        public async Task GetStudyProgram_ReturnExpectedData_WhenDataExists()
        {
            // act
            var institution = await sut.GetInstitution(1);
            var faculty = await sut.GetFaculty(1, institution!);
            var studyProgram = await sut.GetStudyProgram(1, institution!, faculty!);

            // assert
            Assert.NotNull(studyProgram);
        }

        [Fact]
        public async Task GetStudyPrograms_ReturnExpectedData_WhenDataExists()
        {
            // arrange
            Institution? institution = await context.Institutions
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == 1);

            Faculty? faculty = await context.Faculties
                .AsNoTracking()
                .Include(a => a.Institution)
                .FirstOrDefaultAsync(a => a.Id == 1 && a.InstitutionId == institution!.Id);

            // act
            var studyPrograms = await sut.GetStudyPrograms(institution!, faculty!);

            // assert
            Assert.Equal(3, studyPrograms.Count);
        }

        [Fact]
        public async Task GetStudyProgramById_ReturnExpectedData_WhenDataExists()
        {
            // act
            var studyProgram = await sut.GetStudyProgram(1);

            // assert
            Assert.NotNull(studyProgram);
        }

        [Fact]
        public async Task AddInstitution_ReturnExpectedData_WhenDataExists()
        {
            // arrange
            var initialDbCounter = await sut.GetInstitutions();

            // act
            var institutionToAdd = fixture.Create<Institution>();
            institutionToAdd.Id = 10;

            await sut.AddInstitution(institutionToAdd);
            await context.SaveChangesAsync();

            var result = await sut.GetInstitutions();

            // assert
            Assert.Equal(initialDbCounter.Count + 1, result.Count);
        }

        [Fact]
        public async Task AddFaculty_ReturnExpectedData_WhenDataExists()
        {
            // arrange
            Institution? institution = await context.Institutions
            .SingleOrDefaultAsync(a => a.Id == 1);

            // act
            var initialDbFaculties = await sut.GetFaculties(institution!);
            var initialDbFacultiesCounter = initialDbFaculties.Count;

            var facultiesToAdd = fixture.Create<Faculty>();
            facultiesToAdd.Id = 400;
            facultiesToAdd.InstitutionId = institution!.Id;
            facultiesToAdd.Institution = institution;

            await sut.AddFaculty(facultiesToAdd);
            await context.SaveChangesAsync();

            var result = await sut.GetFaculties(institution);

            // assert
            Assert.Equal(initialDbFacultiesCounter + 1, result.Count);
        }

        [Fact]
        public async Task AddStudyProgram_ReturnExpectedData_WhenDataExists()
        {
            // arrange
            Institution? institution = await context.Institutions
                .FirstOrDefaultAsync(a => a.Id == 1);



            Faculty? faculty = await context.Faculties
                  .FirstOrDefaultAsync(a => a.Id == 1);


            // act
            var initialDbCounter = await sut.GetStudyPrograms(institution!, faculty!);

            var studyProgramToAdd = fixture.Create<StudyProgram>();
            studyProgramToAdd.Id = 100;
            studyProgramToAdd.FacultyId = faculty!.Id;
            studyProgramToAdd.Degree = null!;
            studyProgramToAdd.Employees = null;

            await sut.AddStudyProgram(studyProgramToAdd);
            await context.SaveChangesAsync();

            var result = await sut.GetFaculties(institution!);

            // assert
            Assert.Equal(3, result.Count);
        }
    }
}
