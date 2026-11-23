using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentCommandHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);
            var result = await _studentService.AddStudentAsync(student);

            return result == "successfully"
                ? Created("Add Successfully")
                : BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var existingStudent = await _studentService.GetStudentByIdAsync(request.Id);
            if (existingStudent == null)
                return NotFound<string>("Student not found.");

            // Map the incoming editable fields onto the existing tracked entity.
            // This preserves its current entity instance and navigation properties.
            _mapper.Map(request, existingStudent);

            var result = await _studentService.EditStudentAsync(existingStudent);
            return result == "successfully"
                ? Success($"Student {existingStudent.StudID} updated successfully.")
                : BadRequest<string>();
        }
    }
}