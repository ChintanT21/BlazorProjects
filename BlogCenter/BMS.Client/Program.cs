using Blazored.LocalStorage;
using BMS.Client.Components;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7185")
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Validate the server that created the token
            ValidateAudience = true, // Validate the recipient of the token is authorized to receive it
            ValidateLifetime = true, // Check that the token is not expired
            ValidateIssuerSigningKey = true, // Validate the security key used to sign the token
            //ValidIssuer = Configuration["JwtSettings:Issuer"], // Set the server's token issuer
            //ValidAudience = Configuration["JwtSettings:Audience"], // Set the audience intended to receive the token
            //IssuerSigningKey = new SymmetricSecurityKey(key) // Set the key used to sign the token
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpClient();
builder.Services.AddBlazoredLocalStorage();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
