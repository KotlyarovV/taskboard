using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TaskBoard.Data;
using TaskBoard.Infrastucture;

namespace TaskBoard
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<UniqueNameService>()
                .AddSingleton<IFileRepository, FileRepository>()
                .AddSingleton<IFinanceRepository, FinanceRepository>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<IOrderRepository, OrderRepository>()
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => { options.LoginPath = new PathString("/Home/Login"); })
                .Services
                .AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseStaticFiles()
                .UseAuthentication()
                .UseMvcWithDefaultRoute();
        }
    }
}
