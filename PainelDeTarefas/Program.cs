using PainelDeTarefas.Data;
using Microsoft.EntityFrameworkCore;

namespace PainelDeTarefas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<PainelDeTarefasContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PainelDeTarefasContext") ?? throw new InvalidOperationException("Connection string 'PainelDeTarefasContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}