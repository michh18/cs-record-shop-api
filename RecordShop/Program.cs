using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RecordShop.DataModels;
using RecordShop.Repositories;
using RecordShop.Services;

namespace RecordShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // To use in-memory database
            //builder.Services.AddDbContext<RecordShopDbContext>(options => options.UseInMemoryDatabase("RecordShopDb"));

            builder.Services.AddDbContext<RecordShopDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHealthChecks().AddCheck<DbContextHealthCheck<RecordShopDbContext>>("record_shop_health_check");

            builder.Services.AddControllers();
            builder.Services.AddScoped<IAlbumsRepository, AlbumsRepository>();
            builder.Services.AddScoped<IAlbumsService, AlbumsService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseRouting();
            app.MapHealthChecks("/api/health");

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
