using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Turnos.Models;

namespace Turnos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /* Middleware para el manejo de sesiones */
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromSeconds(300); // duracion de la sesion de usuario, 5 minutos -> 300 segundos
                option.Cookie.HttpOnly = true; // alamcena la cookie en el navegador unicamente y no en el equipo del usuario
            });
            services.AddControllersWithViews(option =>
                option.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()) // validacion automatica de tokens GLOBAL Middleware
            );
            services.AddDbContext<TurnosContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TurnosContext")));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) // entorno de desarrollo
            {
                app.UseDeveloperExceptionPage();
            }
            if(env.IsProduction()) // entorno de produccion
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
