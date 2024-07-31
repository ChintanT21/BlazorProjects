using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Dtos.Constant
{
    public static class Constants
    {
        public const string NAVIGATE_TO_CATEGORYLIST_URL = "/admin/categories";
        public const string CATEGORY_EXISTS_ERROR = "Category already exits";
        public const string CATEGORY_NOTADD_ERROR = "Category not added";
        public const string CATEGORY_BLOG_LINKED_ERROR = "Category linked with blogs so delete not possible";
        public const string CATEGORY_NOTDELETE_ERROR = "Category not deleted";

        public const string USER_EXISTS_ERROR = "User already exits";
        public const string USER_NOTADD_ERROR = "User not added";
        public const string USER_NOTFOUND_ERROR = "User not available";
        public const string USER_NOTDELETE_ERROR = "User not deleted";


    }
}
