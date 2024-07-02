using DemoSMSBlazorWebApi.Entity.Dtos;
using DemoSMSBlazorWebApi.Entity.Models;

namespace DemoSMSBlazorWebApi.Server.StudentService
{
    public interface IStudents
    {
         List<Student> getAllStudents();
         Student getStudent(int id);
         void deleteStudent(int id);
         void updateStudent(StudentDto studentDto);
         void addStudent(StudentDto studentDto);
        List<Cource> getAllCources();
    }
}
