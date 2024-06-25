using DemoSMSBlazorWebApi.Entity.Dtos;
using DemoSMSBlazorWebApi.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoSMSBlazorWebApi.Server.StudentService
{
    public class StudentImplementation : IStudent
    {
        private readonly ApplicationDbContext dbContext;

        public StudentImplementation(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Student> getAllStudents()
        {
            try
            {
                return dbContext.Students.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Student getStudent(int id)
        {
            try
            {
                if (id != 0 || id != null)
                {
                    return dbContext.Students.FirstOrDefault(e => e.StudentId == id);


                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) { throw; }
        }


        public void deleteStudent(int id)
        {
            try
            {
                if (id != 0 || id != null)
                {
                    dbContext.Students.Remove(dbContext.Students.FirstOrDefault(e => e.StudentId == id));
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex) { throw; }
        }

        public void updateStudent(StudentDto studentDto)
        {
            if(studentDto != null)
            {
                Student student = new();
                student.StudentId = (int)studentDto.StudentId;
                student.FirstName = studentDto.FirstName;
                student.LastName = studentDto.LastName;
                student.Email = studentDto.Email;
                student.Gender = studentDto.Gender;
                dbContext.Students.Update(student);
                dbContext.SaveChanges();

            }

            
        }
        public void addStudent (StudentDto studentDto)
        {
            try
            {
                if (studentDto.StudentId != 0 || studentDto != null)
                {
                    Student student = new();
                    student.StudentId = (int)studentDto.StudentId;
                    student.FirstName = studentDto.FirstName;
                    student.LastName = studentDto.LastName;
                    student.Email = studentDto.Email;
                    student.Gender = studentDto.Gender;
                    dbContext.Students.Add(student);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex) { throw; }
        }
    }
}
