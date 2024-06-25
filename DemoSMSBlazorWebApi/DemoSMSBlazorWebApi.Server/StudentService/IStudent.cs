using DemoSMSBlazorWebApi.Entity.Dtos;
using DemoSMSBlazorWebApi.Entity.Models;

namespace DemoSMSBlazorWebApi.Server.StudentService
{
    public interface IStudent
    {
        public List<Student> getAllStudents();
        public Student getStudent(int id);
        public void deleteStudent(int id);
        public void updateStudent(StudentDto studentDto);
        public void addStudent(StudentDto studentDto);
    }
}
