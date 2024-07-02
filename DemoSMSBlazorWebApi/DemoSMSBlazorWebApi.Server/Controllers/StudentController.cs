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
        private readonly IStudents studentService;

        public StudentController(IStudents studentService)
        {
           this.studentService = studentService;
        }

        [HttpGet,Route("")]
        public async Task<List<Student>> Get()
        {
            var list= await Task.FromResult(studentService.getAllStudents());
            return list;
        }

        [HttpGet,Route("getCources")]
        public async Task<List<Cource>> GetCources()
        {
            var coursesList = await Task.FromResult(studentService.getAllCources());
            return coursesList;
        }

        [HttpPut]
        public  IActionResult Put(StudentDto studentDto)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            studentService.updateStudent(studentDto);
            return Ok(studentDto);
        }


        [HttpPost]
        public IActionResult Post(StudentDto studentDto)
        {
            if(!ModelState.IsValid) { return BadRequest(); }
            studentService.addStudent(studentDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) {  return BadRequest(); }
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
