using DemoSMSBlazorWebApi.Entity.Dtos;
using DemoSMSBlazorWebApi.Entity.Models;
using DemoSMSBlazorWebApi.Server.StudentService;
using Microsoft.AspNetCore.Mvc;

namespace DemoSMSBlazorWebApi.Server.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudent studentService;

        public StudentController(IStudent studentService)
        {
           this.studentService = studentService;
        }

        [HttpGet]
        public async Task<List<Student>> Get()
        {
            var list= await Task.FromResult(studentService.getAllStudents());
            return list;
        }

        [HttpPut]
        public  IActionResult Put(StudentDto studentDto)
        {
            studentService.updateStudent(studentDto);
            return Ok(studentDto);
        }


        [HttpPost]
        public IActionResult Post(StudentDto studentDto)
        {
            
            studentService.addStudent(studentDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            studentService.deleteStudent(id);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<Student> getStudent(int id)
        {
            return await Task.FromResult(studentService.getStudent(id));
        }
    }
}
