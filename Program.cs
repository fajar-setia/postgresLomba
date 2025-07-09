using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using WebsiteLomba.Data;

namespace WebsiteLomba
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            _ = builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient("LombaApi", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["LombaApiBaseUrl"]);
                client.BaseAddress = new Uri(builder.Configuration["PesertaApiBaseUrl"]);
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Dashboard}/{id?}");

            

            app.Run();
        }
    }
}
