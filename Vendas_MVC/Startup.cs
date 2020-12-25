using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vendas_MVC.Data;
using Vendas_MVC.Services;



namespace Vendas_MVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });




            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Aqui vou ter que substituir a seguir ao options.UseSqlServer por MySQL 
            services.AddDbContext<Vendas_MVCContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("Vendas_MVCContext"), builder =>
                    builder.MigrationsAssembly("Vendas_MVC")));


            services.AddScoped<SeedingService>();  // Vamos registar o nosso serviço, ou seja,isto aqui registra o nosso serviço no sistema de injecçao de dependencia da aplicação.  
            services.AddScoped<SellerService>(); // o nosso serviço agora pode ser injectado noutras classes
            services.AddScoped<DepartmentService>(); // Injeccao de dependencia no sistema
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,SeedingService seedingService)
        {
            // colocar a localização como sendo dos USA
            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions // Em baixo vamos definir as opções de localização
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS }, // Aqui era para saber quais são as localidades para isso criou se uma lista e ai vou colocar nessa lista meu objecto "enUS"
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            // Agora vamos usar em baixo o seguinte comando e depois vamos passar o nosso objecto de localização

            app.UseRequestLocalization(localizationOptions); // o nosso aplicativo agora vai ter como localidade padrão definido os USA





            if (env.IsDevelopment()) /* Aqui vai testar se eu estou no perfil de desenvolv. eu irei fazer uma operação.
                                      Entao neste caso como eu estou no perfil de desenvolvimento eu vou testar aqui o meu SeedingService*/
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed(); // Entao irei popular a minha bd caso ela nao esteja ainda.
            }
            else // mas neste caso aqui se eu ja estiver no perfil de produção, eu irei fazer outras aplicações
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
