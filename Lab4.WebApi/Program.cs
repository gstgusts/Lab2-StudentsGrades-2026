
using Lab2.DataAccess;
using Lab2.DataAccess.Interfaces;
using Lab2.DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Lab4.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var dataDir = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "Lab2.DataAccess"));
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDir);

            var cs = builder.Configuration.GetConnectionString("StudentDatabase")
                     ?? throw new InvalidOperationException("Missing ConnectionStrings:StudentDatabase");
            if (cs.StartsWith("\"") && cs.EndsWith("\"")) cs = cs.Trim('"');

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<StudentDbContext>(o => o.UseSqlServer(cs));

            builder.Services.AddScoped<IStudentRepository, StudentRepositoryService>();

            // enable cors
            builder.Services.AddCors(o =>
            {
                o.AddDefaultPolicy(p => p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            //https://localhost:7213/openapi/v1.json

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                // https://localhost:7213/swagger/index.html
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1");
                });

                // https://localhost:7213/api-docs/
                app.UseReDoc(options =>
                {
                    options.SpecUrl("/openapi/v1.json");
                });

                // https://localhost:7213/scalar/
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // default cors policy
            app.UseCors();
            app.MapControllers();

            app.Run();
        }
    }
}
