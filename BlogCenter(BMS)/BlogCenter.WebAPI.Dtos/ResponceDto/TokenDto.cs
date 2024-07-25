using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Dtos.ResponceDto
{
    public class TokenDto
    {
        public bool IsAuthenticated { get; set; } 
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
