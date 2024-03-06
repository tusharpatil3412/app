
using ClassLibrary.data;
using ClassLibrary.repo;
using System.Threading.RateLimiting;

namespace Application
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
            builder.Services.AddTransient<IDataAcc, DataAcc>();
            builder.Services.AddTransient<IAdminrepo, Adminrepo>();
            builder.Services.AddTransient<IEmployeerepo,Employeerepo>();
            builder.Services.AddTransient<IRecordrepo, Recordrepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
