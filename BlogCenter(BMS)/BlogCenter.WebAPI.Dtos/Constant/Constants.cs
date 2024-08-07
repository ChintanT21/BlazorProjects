using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCenter.WebAPI.Dtos.Constant
{
    public static class Constants
    {
        public const string NAVIGATE_TO_LOGIN_URL = "/login";
        public const string NAVIGATE_TO_REGISTER_URL = "/register";

        public const string NAVIGATE_TO_CATEGORYLIST_URL = "/admin/categories";
        public const string CATEGORY_EXISTS_ERROR = "Category already exits";
        public const string CATEGORY_NOTADD_ERROR = "Category not added";
        public const string CATEGORY_ADDED_SUCCESS = "Category is added successfully";
        public const string CATEGORY_BLOG_LINKED_ERROR = "Category linked with blogs so delete not possible";
        public const string CATEGORY_NOTDELETE_ERROR = "Category not deleted";

        public const string USER_EXISTS_ERROR = "User already exits";
        public const string USER_NOTADD_ERROR = "User not added";
        public const string USER_ADDED_SUCCESS = "User is added successfully";
        public const string USER_NOTFOUND_ERROR = "User not available";
        public const string USER_NOTDELETE_ERROR = "User not deleted";
        public const string NAVIGATE_TO_USERLIST_URL = "/admin/users";

        public const string NAVIGATE_TO_BLOGLIST_URL = "/admin/blogs";
        public const string BLOG_NOCATEGORY_ERROR = "Select Atleast one category.";
        public const string BLOG_NOCONTENT_ERROR = "Content can not be empty.";
        public const string BLOG_EXISTS_ERROR = "Blog already exits";
        public const string BLOG_NOTADD_ERROR = "Blog not added";
        public const string BLOG_ADDED_SUCCESS = "Blog is added successfully";
        public const string BLOG_BLOG_LINKED_ERROR = "Blog linked with blogs so delete not possible";
        public const string BLOG_NOTDELETE_ERROR = "Blog not deleted";





    }
}
