using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.DTO;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Response<List<GetStudentListResponse>>>
    {

    }
}
