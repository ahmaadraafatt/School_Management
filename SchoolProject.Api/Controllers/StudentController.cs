using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class StudentController : AppControllerBase
    {
        [HttpGet(Router.studentRouting.List)]
        public async Task<IActionResult> GetStudentsList()
        {
            var response = await _mediatR.Send(new GetStudentListQuery());
            return NewResult(response);
        }
        [HttpGet(Router.studentRouting.GetById)]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var response = await _mediatR.Send(new GetStudentByIdQuery(id));
            return NewResult(response);
        }
        [HttpPost(Router.studentRouting.Create)]
        public async Task<IActionResult> Creat([FromBody] AddStudentCommand command)
        {
            var response = await _mediatR.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.studentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand command)
        {
            var response = await _mediatR.Send(command);
            return NewResult(response);
        }
    }
}