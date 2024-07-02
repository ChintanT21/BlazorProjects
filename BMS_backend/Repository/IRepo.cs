using BMS_backend.Models;
using BMS_backend.ViewModels;

namespace BMS_backend.Repository
{
    public interface IRepo
    {
        Task<ApiResponse> AddAsync(StudentDto studentDto);
        Task<ApiResponse> DeleteAsync(int id);
    }
}
