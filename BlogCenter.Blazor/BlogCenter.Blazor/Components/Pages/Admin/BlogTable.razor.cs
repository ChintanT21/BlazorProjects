using BlogCenter.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using static BlogCenter.WebAPI.Dtos.RequestDto.GetBlogDto;

namespace BlogCenter.Blazor.Components.Pages.Admin
{
    public partial class BlogTable : ComponentBase
    {
        [Inject]
        private IClientService _clientService { get; set; }

        private List<GetBlog>? blogs;
        private IQueryable<GetBlog>? blogsQueryable;
        private PaginationState pagination = new PaginationState { ItemsPerPage = 15 };
        private string nameFilter = string.Empty;

        private GridSort<GetBlog> rankSort = GridSort<GetBlog>
            .ByDescending(x => x.Title)
            .ThenDescending(x => x.Content)
            .ThenDescending(x => x.StatusName);

        private IQueryable<GetBlog>? FilteredBlogs => blogsQueryable?
            .Where(x => x.Title.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase));

        protected async override Task OnParametersSetAsync()
        {
            blogs = await _clientService.GetBlogData();
            blogsQueryable=blogs.AsQueryable();
        }

    }
}