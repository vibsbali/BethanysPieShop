using BethanysPieShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BethanysPieShop
{
   public class Startup
   {
      private readonly IConfiguration _configuration;

      public Startup(IConfiguration configuration)
      {
         _configuration = configuration;
      }
      // This method gets called by the runtime. Use this method to add services to the container.
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

         //Adding Identity Management 
         services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

         services.AddScoped<IPieRepository, PieRepository>();
         services.AddScoped<IFeedbackRepository, FeedbackRepository>();

         services.AddMvc();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
         }
         
         app.UseStaticFiles();

         app.UseAuthentication();

         //Second last before Authentication
         app.UseMvc(route => { route.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });

         app.Run(async (context) =>
         {
            await context.Response.WriteAsync("Hello World!");
         });
      }
   }
}
