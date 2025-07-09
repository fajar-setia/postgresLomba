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

            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient("LombaApi", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["LombaApiBaseUrl"]);
            });

            builder.Services.AddHttpClient("PesertaApi", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["PesertaApiBaseUrl"]);
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Swagger aktif di semua environment
            app.UseSwagger();
            app.UseSwaggerUI();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();
            app.MapGet("/", () => "API is running 🚀");
            app.MapGet("/ping", () => "pong");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Dashboard}/{id?}");

            app.Run();
        }
    }
}
