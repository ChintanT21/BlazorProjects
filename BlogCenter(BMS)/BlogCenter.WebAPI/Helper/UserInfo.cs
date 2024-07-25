using BlogCenter.WebAPI.Dtos.ResponceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Dtos.Helper
{
    public class UserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long UserId
        {
            get
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null && httpContext.Items.TryGetValue("TokenDto", out var tokenDtoObj) && tokenDtoObj is TokenDto tokenDto)
                {
                    return tokenDto.Id;
                }

                return 0; 
            }
        }
    }
}
