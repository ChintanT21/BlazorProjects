using BlogCenter.WebAPI.Models.Models;
using BlogCenter.WebAPI.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Repositories.User
{
    public class UserRepository(ApplicationDbContext _dbContext):BaseRepository<Models.Models.User>(_dbContext),IUserRepository
    {
    }
}
