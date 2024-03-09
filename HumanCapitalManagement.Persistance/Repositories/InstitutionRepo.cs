using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace HumanCapitalManagement.Persistance.Repositories
{
    public class InstitutionRepo : IInstitutionRepo
    {
        private readonly ApplicationDbContext _context;

        public InstitutionRepo(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddInstitution(Institution institution)
        {
            Log.Information("[{class}.{method}] has been called, adding an institution having to the context.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

            await _context.AddAsync(institution);
        }

        public async Task AddFaculty(Faculty faculty)
        {
            Log.Information("[{class}.{method}] has been called, adding a faculty to the context.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

            await _context.Faculties.AddAsync(faculty);
        }

        public async Task AddStudyProgram(StudyProgram studyProgram)

        {
            Log.Information("[{class}.{method}] has been called, adding a study program to the context.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

            await _context.StudyPrograms.AddAsync(studyProgram);
        }

        public async Task<Institution?> GetInstitution(int id)
        {
            Institution? institution = await _context.Institutions
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            Log.Information("[{class}.{method}] has been called, retrieving an institution from the context with the following details: {institution}.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), JsonConvert.SerializeObject(institution));

            return institution;
        }

        public async Task<Faculty?> GetFaculty(int facultyId, Institution institution)
        {
            Faculty? faculty = await _context.Faculties
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == facultyId && a.InstitutionId == institution.Id);

            Log.Information("[{class}.{method}] has been called, retrieving a faculty from the context with the following details: {faculty}.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), JsonConvert.SerializeObject(faculty));

            return faculty;
        }

        public async Task<ICollection<Institution>> GetInstitutions()
        {
            ICollection<Institution> institutions = await _context.Institutions
                .AsNoTracking()
                .ToListAsync();

            Log.Information("[{class}.{method}] has been called, retrieving a number of {count} entities.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), institutions.Count);

            return institutions;
        }

        public async Task<ICollection<Faculty>> GetFaculties(Institution institution)
        {
            ICollection<Faculty> faculties = await _context.Faculties
                .AsNoTracking()
                .Where(a => institution.Id == a.InstitutionId)
                .ToListAsync();

            Log.Information("[{class}.{method}] has been called, retrieving a number of {count} entities.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), faculties.Count);

            return faculties;
        }

        public async Task<ICollection<StudyProgram>> GetStudyPrograms(Institution institution, Faculty faculty)
        {
            ICollection<StudyProgram> studyPrograms = await _context.StudyPrograms
                .AsNoTracking()
                .Where(a => faculty.Id == a.FacultyId && institution.Id == faculty.InstitutionId)
                .ToListAsync();

            Log.Information("[{class}.{method}] has been called, retrieving a number of {count} entities.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), studyPrograms.Count);

            return studyPrograms;
        }

        public async Task<StudyProgram?> GetStudyProgram(int studyProgramId, Institution institution, Faculty faculty)
        {
            StudyProgram? studyProgram = await _context.StudyPrograms
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == studyProgramId && faculty.Id == a.FacultyId && institution.Id == faculty.InstitutionId);

            Log.Information("[{class}.{method}] has been called, retrieving a studyProgram from the context with the following details: {studyProgram}.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), JsonConvert.SerializeObject(studyProgram));

            return studyProgram;
        }

        public async Task<StudyProgram?> GetStudyProgram(int studyProgramId)
        {
            StudyProgram? studyProgram = await _context.StudyPrograms
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == studyProgramId);

            Log.Information("[{class}.{method}] has been called, retrieving a studyProgram from the context with the following details: {studyProgram}.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), JsonConvert.SerializeObject(studyProgram));

            return studyProgram;
        }

        public async Task<StudyProgram?> GetStudyProgramForEmployee(int employeeId)
        {
            StudyProgram? studyProgram = await _context.StudyPrograms
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Employees.Any(p => p.EmployeeId == employeeId));

            Log.Information("[{class}.{method}] has been called, retrieving a studyProgram from the context with the following details: {studyProgram}.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), JsonConvert.SerializeObject(studyProgram));

            return studyProgram;
        }
    }
}
