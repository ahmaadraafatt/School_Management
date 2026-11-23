using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validators
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        #region Fields
        private readonly IStudentService _studentservice;
        #endregion

        #region Constractor
        public EditStudentValidator(IStudentService studentservice)
        {
            _studentservice = studentservice;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Methods
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must not be Empty")
                .NotNull().WithMessage("Name must not be Null")
                .MaximumLength(20).WithMessage("MaximumLength is 10");
            RuleFor(x => x.Address)
              .NotEmpty().WithMessage("Address must not be Empty")
              .NotNull().WithMessage("Address must not be Null");


        }
        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (model, key, CancellationToken) => !await _studentservice.IsStudentNameExistExcludeSelf(key, model.Id))
                .WithMessage("Name is already exist");
        }
        #endregion

    }
}
