using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Dtos.ResponceDto
{
    public class DataManipulationDto
    {
        public string SearchString { get; set; } = ""; 
        public string SortString { get; set; } = ""; 
        public string SearchTable { get; set; } = "Title"; 
        public long? UserId { get; set; } 
        public int PageSize { get; set; } = 10; 
        public int PageNumber { get; set; } = 1; 
    }
}
