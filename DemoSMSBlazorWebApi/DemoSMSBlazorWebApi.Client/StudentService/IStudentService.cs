using DemoSMSBlazorWebApi.Entity.Dtos;
using DemoSMSBlazorWebApi.Entity.Models;
using DemoSMSBlazorWebApi.Server.StudentService;

namespace DemoSMSBlazorWebApi.Client.StudentService
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllStudents();
    }
}
