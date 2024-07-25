using BlogCenter.WebAPI.Dtos;
using BlogCenter.WebAPI.Dtos.Mapper;
using BlogCenter.WebAPI.Dtos.RequestDto;
using BlogCenter.WebAPI.Filters;
using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Services.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlogCenter.WebAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService _categoryService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Response<GetCategoryDto> response = new();
            List<Models.Models.Category> categories = await _categoryService.GetAll();
            if (categories != null)
            {
                response = new()
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    ResultList = categories.MapToGetCategoryDtoList()
                };
                return Ok(response);
            }
             response = new()
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.NoContent,
                ErrorMessages = ["No Content"]
            };
            return Ok(response);
        }

    }
}
