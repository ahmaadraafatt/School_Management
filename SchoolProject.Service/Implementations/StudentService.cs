using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    internal class StudentService : IStudentService
    {
        #region fields and constructor
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        #endregion
        #region Methods
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetStudentsListAsync();
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            return student;
        }

        async Task<string> IStudentService.AddStudentAsync(Student student)
        {
            await _studentRepository.AddAsync(student);
            return "successfully";
        }
        public async Task<bool> IsStudentNameExist(string name)
        {
            var student = await _studentRepository.GetTableNoTracking().Where(s => s.Name.Equals(name)).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsStudentNameExistExcludeSelf(string name, int id)
        {
            var student = await _studentRepository.GetTableNoTracking().Where(s => s.Name.Equals(name) & s.StudID.Equals(id)).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }

        public async Task<string> EditStudentAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "successfully";
        }

        #endregion
    }
}