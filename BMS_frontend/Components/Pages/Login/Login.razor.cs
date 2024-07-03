using BMS_frontend.Dtos;
using Microsoft.AspNetCore.Components;

namespace BMS_frontend.Components.Pages.Login
{
    public partial class Login
    {
        protected LoginDto loginDto = new();  

        public void AuthenticateUser()
        {
            if (loginDto is null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

        }

    }
}
