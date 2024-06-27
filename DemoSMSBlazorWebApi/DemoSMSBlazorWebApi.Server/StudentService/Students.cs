using DemoSMSBlazorWebApi.Entity.Dtos;
using DemoSMSBlazorWebApi.Entity.Models;

namespace DemoSMSBlazorWebApi.Server.StudentService
{
    public class Students : IStudents
    {
        private readonly ApplicationDbContext dbContext;

        public Students(ApplicationDbContext dbContext)
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
                if (id != 0)
                {
                    var isAvailable = dbContext.Students.Any(e => e.StudentId == id);
                    if (isAvailable)
                    {
                        return dbContext.Students.FirstOrDefault(e => e.StudentId == id);
                    }
                    else
                    {
                        return null;
                    }
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
                if (id != 0)
                {
                    var isAvailable = dbContext.Students.Any(e => e.StudentId == id);
                    if (isAvailable)
                    {
                        dbContext.Students.Remove(dbContext.Students.FirstOrDefault(e => e.StudentId == id));
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex) { throw; }
        }
        public void updateStudent(StudentDto studentDto)
        {
            try
            {
                if (studentDto != null)
                {
                    Student student = new();
                    student.StudentId = studentDto.StudentId;
                    student.FirstName = studentDto.FirstName;
                    student.LastName = studentDto.LastName;
                    student.Email = studentDto.Email.ToLower();
                    student.Gender = studentDto.Gender;
                    dbContext.Students.Update(student);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex) { throw; }


        }
        public void addStudent(StudentDto studentDto)
        {
            try
            {
                if (studentDto.StudentId != 0 || studentDto != null)
                {
                    Student student = new();
                    student.StudentId = studentDto.StudentId;
                    student.FirstName = studentDto.FirstName;
                    student.LastName = studentDto.LastName;
                    student.Email = studentDto.Email.ToLower();
                    student.Gender = studentDto.Gender;
                    dbContext.Students.Add(student);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex) { throw; }
        }
    }
}
