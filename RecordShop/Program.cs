using Microsoft.EntityFrameworkCore;
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

            // Add services to the container.
            builder.Services.AddDbContext<RecordShopDbContext>(options => options.UseInMemoryDatabase("RecordShopDb"));

            builder.Services.AddControllers();
            builder.Services.AddScoped<IAlbumsRepository, AlbumsRepository>();
            builder.Services.AddScoped<IAlbumsService, AlbumsService>();

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

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
