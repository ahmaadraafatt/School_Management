using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Repositories
{
    public class StudentRepository :GenericRepositoryAsync<Student>, IStudentRepository
    {
        #region Fields,Constructor

        private readonly DbSet<Student> _students;

        public StudentRepository(ApplicationDbContext dBcontext) : base(dBcontext) 
        {
            _students = dBcontext.Set<Student>();
        }
       
        #endregion
        #region Methods 
        public async Task<List<Student>> GetStudentsListAsync()
        {
           return await _students.Include(s => s.Department).ToListAsync();
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _students.Include(s => s.Department).FirstOrDefaultAsync(s => s.StudID == id);
        }
        #endregion
    }
}
