using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Dtos.Enums
{
    public class Enums
    {
        public enum BlogStatus
        {
            Draft = 1,
            SendForApproval = 2,
            Approved = 3,
            Rejected = 4,
            Deleted = 5
        }

    }
}
