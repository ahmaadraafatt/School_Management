using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>,
                                                         IRequestHandler<EditStudentCommand, Response<string>>
    {


        #region fields 
        public readonly IStudentService _studentService;
        public readonly IMapper _mapper;
        #endregion
        #region constractor
        public StudentCommandHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion
        #region Methods
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapper = _mapper.Map<Student>(request);
            var result = await _studentService.AddStudentAsync(studentMapper);
            if (result == "successfully") return Created("Add Successfully");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null)
                return NotFound<string>("Student not found");
            var studentMapper = _mapper.Map<Student>(request);
            var result = await _studentService.EditStudentAsync(studentMapper);
            if (result == "successfully") return Success($"Edit Successfully{studentMapper.StudID}");
            else return BadRequest<string>();
        }
        #endregion
    }
}
