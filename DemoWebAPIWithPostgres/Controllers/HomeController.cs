using AutoMapper;
using DemoWebAPIWithPostgres.Dto;
using DemoWebAPIWithPostgres.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPIWithPostgres.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public HomeController(ApplicationDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAllStudent()
        {

            var students = dbContext.Students.ToList();
            if (students is null)
            {
                return NotFound();
            }
            return Ok(students.Select(student =>mapper.Map<AddStudentDto>(student)));
        }

        [HttpGet("{id:int}")]
        public ActionResult GetStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest(string.Empty);
            }
            var student = dbContext.Students.Find(id);
            if (student is null)
            {
                return BadRequest();
            }
            return Ok(student);
        }
        [HttpPost]
        public ActionResult AddStudent([FromBody] AddStudentDto addStudentDto)
        {
            if (addStudentDto is null)
            {
                return NotFound();
            }
            //Student student = new()
            //{
            //    FirstName = addStudentDto.FirstName,
            //    LastName = addStudentDto.LastName,
            //}; autoMapper Replace this part

            var newStudent = mapper.Map<Student>(addStudentDto);

            dbContext.Students.Add(newStudent);
            dbContext.SaveChanges();
            return Ok(addStudentDto);
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest(string.Empty);
            }
            var student = dbContext.Students.Find(id);
            if (student is null)
            {
                return NotFound(string.Empty);
            }
            dbContext.Remove(student);
            dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateStudent(int id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            try
            {
                if (id == 0 || updateStudentDto is null)
                {
                    return BadRequest(string.Empty);
                }

                var student = dbContext.Students.Find(id);
                if (student is null)
                {
                    return NotFound();
                }
                student.FirstName= updateStudentDto.FirstName;
                student.LastName= updateStudentDto.LastName;
                student.Email= updateStudentDto.Email;
                student.Cource= updateStudentDto.Cource;
                student.CourceId= updateStudentDto.CourceId;

                dbContext.Students.Update(student);
                dbContext.SaveChanges();
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
