using DemoSMSBlazorWebApi.Entity.Dtos;
using DemoSMSBlazorWebApi.Entity.Models;
using Microsoft.AspNetCore.Components;
using static System.Net.WebRequestMethods;

namespace DemoSMSBlazorWebApi.Client.Components.Pages
{
    public class AddEditUserBase : ComponentBase
    {
        [Parameter]
        public int? id { get; set; }

        [Inject]
        HttpClient? Client { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Student student = new();
        public StudentDto studentDto = new();

        public string Title = "Add Student";

        public DateOnly today = DateOnly.FromDateTime(DateTime.Now);


        protected override async Task OnParametersSetAsync()
        {
            if (id != 0 && id!=null)
            {
                Title = "Edit Student";
                string apiUrl = $"api/student/{id}";
                student = await Client.GetFromJsonAsync<Student>(apiUrl);
                studentDto.StudentId = student.StudentId; 
                studentDto.FirstName = student.FirstName;
                studentDto.LastName=student.LastName;
                studentDto.Email = student.Email;
                studentDto.Gender = student.Gender;

            }
        }



        public async Task AddStudent()
        {
            if(studentDto.StudentId != 0)
            {
                await Client.PutAsJsonAsync("api/student", studentDto);
            }
            else
            {
                await Client.PostAsJsonAsync("api/student", studentDto);
            }
            Cancel();
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("/");
        }


    }
}
