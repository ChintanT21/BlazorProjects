using BMS_backend.Models;
using BMS_backend.Repository;
using BMS_backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net;

namespace BMS_backend.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{v:apiversion}/[controller]")]
    public class StudentController : Controller
    {
        private readonly Repository.IRepository<Student> _genericRepository;
        private readonly IRepo _repository;

        public StudentController(IRepository<Student> GenericRepository, IRepo repository)
        {
            _genericRepository = GenericRepository;
            _repository = repository;
        }


        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("getAllStudentsList")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> Get()
        {
            ApiResponse apiResponse = new();
            try
            {
                apiResponse = await _genericRepository.GetAllAsync();
                return Ok(apiResponse);

            }
            catch (Exception ex) { return BadRequest(); }
        }

        [HttpGet("GetStudentById/{id:int}")]
        public async Task<ActionResult<ApiResponse>> GetStudentById(int id)
        {
            try
            {
                ApiResponse apiResponse = await _genericRepository.GetByIdAsync(id);
                if (apiResponse == null)
                {
                    return NotFound();
                }

                return Ok(apiResponse);
            }
            catch (Exception ex) { return StatusCode(500, $"Internal server error: {ex.Message}"); }
        }

        [HttpDelete("DeleteStudentById/{id:int}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            try
            {
                ApiResponse apiResponse = await _repository.DeleteAsync(id);
                return Ok(apiResponse);
            }
            catch (Exception ex) { return BadRequest(); };

        }

        [HttpPost("AddStudent")]
        public async Task<ActionResult<ApiResponse>> Post(StudentDto studentDto)
        {
            try
            {
                ApiResponse apiResponse = await _repository.AddAsync(studentDto);
                return Ok(apiResponse);
            }
            catch (Exception ex) { return BadRequest(); };
        }

        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<ActionResult<ApiResponse>> Put(Student student)
        {
            ApiResponse apiResponse = new();
            if (ModelState.IsValid!){
                apiResponse = new()
                {
                    StatusCode = HttpStatusCode.NotAcceptable,
                    IsSuccess = false,
                    ErrorMessages = ["model state is not valid"]
                };
            }
            apiResponse = await _genericRepository.UpdateAsync(student);
            return apiResponse;

        }

    }
}

