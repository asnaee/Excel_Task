
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using work_01.Allergie_REPO;
using work_01.Disease_REPO;
using work_01.Model;
using work_01.Ncd_REPO;
using work_01.Patien_Repo;

namespace work_01
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
            builder.Services.AddDbContext<AsigmentContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("GF")));
            builder.Services.AddScoped<Incd, NcdRepo>();
            builder.Services.AddScoped<IAllergie, AllergieREPO>();
            builder.Services.AddScoped<IDisease, DiseaseInformationREPO>();
            builder.Services.AddScoped<IPatient, PatientREPO>();
            builder.Services.AddCors(x => x.AddPolicy("df", p => p.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                // Other JSON options can be set here...
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("df");
            app.UseAuthorization();
           

            app.MapControllers();

            app.Run();
        }
    }
}