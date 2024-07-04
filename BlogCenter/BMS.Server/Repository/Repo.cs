using BMS.Server.Models;
using BMS.Server.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BMS.Server.Repository
{
    public class Repo : IRepo
    {
        public readonly ApplicationDbContext _dbcontext;
        private readonly IRepository<Student> _studentRepository;

        public Repo(ApplicationDbContext dbcontext, IRepository<Student> studentRepository) 
        {
            _dbcontext = dbcontext;
            _studentRepository = studentRepository;
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            ApiResponse apiResponse = new();
            try
            {
                if (id <= 0)
                {
                    return apiResponse = new()
                    {
                        IsSuccess = false,
                        ErrorMessages = ["id- " + id + " is not valid"],
                        StatusCode = HttpStatusCode.BadRequest,
                    };
                }

                Student student =await _dbcontext.Students.FindAsync(id);
                if (student != null)
                {
                    _studentRepository.DeleteAsync(student);
                    return apiResponse = new()
                    {
                        IsSuccess = true,
                        StatusCode = HttpStatusCode.OK,
                        Result = student,
                    };
                }
                else
                {
                    return apiResponse = new()
                    {
                        IsSuccess = false,
                        ErrorMessages = ["student not found at id: " + id],
                        StatusCode = HttpStatusCode.NoContent,
                    };
                }
            }
            catch (Exception ex) {
                return apiResponse = new()
                {
                    IsSuccess = false,
                    ErrorMessages = ["SREVER ERROR"],
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }

        Task<ApiResponse> IRepo.AddAsync(StudentDto studentDto)
        {
            Student student = new()
            {
                FirstName=studentDto.FirstName,
                LastName=studentDto.LastName,
                Email=studentDto.Email,
                Gender=studentDto.Gender,
            };
            return _studentRepository.AddAsync(student);
        }
    }
}
