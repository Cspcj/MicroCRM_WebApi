using Microsoft.EntityFrameworkCore;
using MCRMWebApi.DataContext;
using MCRMWebApi.DTOs;
using MCRMWebApi.Repostories;
using MCRMWebApi.Services;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;

namespace MCRMWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            

            builder.Services.AddDbContext<MCRMDbContext>(options =>
                           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // add automapper service
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            // add services for repositories
            builder.Services.AddTransient<IUsersRepository, UsersRepository>();
            builder.Services.AddTransient<ITasksRepository, TasksRepository>();
            builder.Services.AddTransient<IProjectsRepository, ProjectsRepository>();
            builder.Services.AddTransient<INotesRepository, NotesRepository>();
            builder.Services.AddTransient<IClientsRepository, ClientsRepository>();
            builder.Services.AddTransient<IEmployeesRepository, EmployeesRepository >();
            builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

            // add services
            builder.Services.AddTransient<ITasksService, TasksService>();
            builder.Services.AddTransient<IUserService, UsersService>();
            builder.Services.AddTransient<IProjectsService, ProjectsService>();
            builder.Services.AddTransient<INotesService, NotesService>();
            builder.Services.AddTransient<IClientsService, ClientsService>();
            builder.Services.AddTransient<IEmployeesService, EmployeesService>();


            //JWT
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Bearer Security Scheme \r\n\r\n" +
                            "Enter 'Bearer'[space] and your token in the input text below"
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Authentication:Domain"],
                        ValidAudience = builder.Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Secret"]))
                    };
                });
          

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
        }
    }
}