using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlogCenter.WebAPI.Dtos.Enums.Enums;

namespace BlogCenter.WebAPI.Dtos.RequestDto
{
    public class GetUserDto
    {
        public long Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? ProfileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public short Status { get; set; }
        public UserStatus? StatusName { get; set; }
    }
}
