using BlazorCrud.Shared.DataModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Pages
{
    public class AddEditEmployeeBase : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int empID { get; set; }

        protected string Title = "Add";
        public Employee employee = new();
        protected List<City> cityList = new();

        protected override async Task OnInitializedAsync()
        {
            await GetCityList();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (empID != 0)
            {
                Title = "Edit";
                employee = await Http.GetFromJsonAsync<Employee>("api/Employee/" + empID);
            }
        }

        protected async Task GetCityList()
        {
            cityList =await  Http.GetFromJsonAsync<List<City>>("api/Employee/GetCityList");
        }

        protected async Task SaveEmployee()
        {
            if (employee.Employeeid != 0)
            {
                await Http.PutAsJsonAsync("api/Employee", employee);
            }
            else
            {
                await Http.PostAsJsonAsync("api/Employee", employee);
            }
            Cancel();
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("/fetchemployee");
        }
    }
}
