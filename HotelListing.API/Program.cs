using HotelListing.API.Configurations;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace HotelListing.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // rb
            var hotelListingDbConStr = builder.Configuration.GetConnectionString("HotelListingDbConStr");
            builder.Services.AddDbContext<HotelListingDbContext>(options => {
                options.UseSqlServer(hotelListingDbConStr);
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ================================================================
            // rb start
            // ================================================================
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", b => b.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod());
            });

            builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration
            (ctx.Configuration));

            builder.Services.AddAutoMapper(typeof(MapperConfig));
            // ================================================================
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}