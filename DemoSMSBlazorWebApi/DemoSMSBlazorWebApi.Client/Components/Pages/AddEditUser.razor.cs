using DemoSMSBlazorWebApi.Entity.Dtos;
using DemoSMSBlazorWebApi.Entity.Models;
using Microsoft.AspNetCore.Components;

namespace DemoSMSBlazorWebApi.Client.Components.Pages
{
    public class AddEditUserBase : ComponentBase
    {
        [Parameter]
        public int? Id { get; set; }

        [Inject]
        HttpClient httpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Student student = new();
        public StudentDto studentDto = new();

        public string Title = "Add Student";

        public DateOnly today = DateOnly.FromDateTime(DateTime.Now);


        protected override async Task OnParametersSetAsync()
        {
            if (Id != 0 && Id != null)
            {
                Title = "Edit Student";
                string apiUrl = $"api/student/{Id}";
                studentDto = await httpClient.GetFromJsonAsync<StudentDto>(apiUrl);

            }
        }



        public async Task AddStudent()
        {
            if (studentDto.StudentId != 0)
            {
                await httpClient.PutAsJsonAsync("api/student", studentDto);
            }
            else
            {
                await httpClient.PostAsJsonAsync("api/student", studentDto);
            }
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("/");
        }


    }
}
