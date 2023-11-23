using CapstoneDb.Data;
using CapstoneDb.Services;
using Microsoft.EntityFrameworkCore;

namespace CapstoneDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });


            string connectionString = "Server=localhost;port=3306;Database=CapstoneDb;User=root;";

            // Register the DbContext as a scoped service
            builder.Services.AddDbContext<CapstoneDbContext>(options => options.UseMySQL(connectionString));
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<PostRepository>();
            builder.Services.AddScoped<CommentRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            // Enable CORS
            app.UseCors("AllowAllOrigins");


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}