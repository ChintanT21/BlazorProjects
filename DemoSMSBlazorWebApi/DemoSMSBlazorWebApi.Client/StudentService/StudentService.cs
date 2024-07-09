using DemoSMSBlazorWebApi.Entity.Dtos;
using DemoSMSBlazorWebApi.Entity.Models;
using DemoSMSBlazorWebApi.Server.StudentService;

namespace DemoSMSBlazorWebApi.Client.StudentService
{
    public class StudentService(HttpClient httpClient) : IStudentService
    {
        async Task<List<Student>> IStudentService.GetAllStudents()
        {
            string url = $"api/student";
            return await httpClient.GetFromJsonAsync<List<Student>>(url);
        }
    }
}
