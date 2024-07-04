using BMS.Server.Auth;
using BMS.Server.Models;
using BMS.Server.Repository;
using idenitywebapiauthenitcation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddScoped<IRepo, Repo>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(option =>
//{
//    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
//    {
//        Title = "Auth Demo",
//        Version = "v1"
//    });

//    option.AddSecurityDefinition("bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//    {
//        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//        Description = "Please Enter a token",
//        Name = "Authorization",
//        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
//        BearerFormat = "JWT",
//        Scheme = "bearer"

//    });

//    option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
//    {
//        {
//        new OpenApiSecurityScheme
//        {
//            Reference =new OpenApiReference
//            {
//                Type=ReferenceType.SecurityScheme,
//                Id="Bearer"
//            }
//        },
//        []
//        }
//    });
//});

//builder.Services.AddSwaggerGen(option =>
//{
//    option.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//    {
//        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey
//    });
//    option.OperationFilter<SecurityRequirementsOperationFilter>();
//});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key-here"))
        };
    });

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
});

builder.Services.AddAuthorization();


var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();


