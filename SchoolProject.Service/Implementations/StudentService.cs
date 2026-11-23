using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    internal class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetStudentsListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetStudentByIdAsync(id);
        }

        public async Task<string> AddStudentAsync(Student student)
        {
            await _studentRepository.AddAsync(student);
            return "successfully";
        }

        public async Task<bool> IsStudentNameExist(string name)
        {
            return await _studentRepository.GetTableNoTracking()
                .AnyAsync(s => s.Name == name);
        }

        public async Task<bool> IsStudentNameExistExcludeSelf(string name, int id)
        {
            return await _studentRepository.GetTableNoTracking()
                .AnyAsync(s => s.Name == name && s.StudID != id);
        }

        public async Task<string> EditStudentAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "successfully";
        }
    }
}