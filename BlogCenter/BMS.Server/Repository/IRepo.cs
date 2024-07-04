using BMS.Server.Models;
using BMS.Server.ViewModels;

namespace BMS.Server.Repository
{
    public interface IRepo
    {
        Task<ApiResponse> AddAsync(StudentDto studentDto);
        Task<ApiResponse> DeleteAsync(int id);
    }
}
