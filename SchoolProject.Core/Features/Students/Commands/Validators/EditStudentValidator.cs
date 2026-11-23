using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validators
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentService _studentService;

        public EditStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;

            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Student ID must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must not be empty.")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address must not be empty.")
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");

            RuleFor(x => x.Phone)
                .MaximumLength(100).WithMessage("Phone cannot exceed 100 characters.");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0)
                .WithMessage("Department ID must be greater than zero.");

            RuleFor(x => x.Name)
                .MustAsync(async (command, name, cancellationToken) =>
                    !await _studentService.IsStudentNameExistExcludeSelf(name, command.Id))
                .WithMessage("A student with this name already exists.");
        }
    }
}