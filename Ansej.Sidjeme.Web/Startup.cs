using Ansej.Business.Core;
using Ansej.Data.Abstractions;
using Ansej.Data.Core;
using Ansej.Sidjeme.Business;
using Ansej.Sidjeme.Data;
using Ansej.Sidjeme.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Ansej.Sidjeme.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization();

            services.AddControllersWithViews()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(o =>
                {
                    o.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        return factory.Create(typeof(Labels));
                    };
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddScoped<DbContext, PortailNedaDbContext>();
            services.AddDbContext<PortailNedaDbContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("PortailNesdaDbConnection")));

            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepository<,>), typeof(GenericRepository<,>));
            services.AddTransient(typeof(GenericBusinessService<,>));
            services.AddTransient(typeof(ReferentielBusinessService<,>));
            services.AddTransient(typeof(WilayaBusinessService));
            services.AddTransient(typeof(ContactUsBusinessService));
            services.AddTransient(typeof(StructureManagerBusinessService));
            services.AddTransient(typeof(ConsultationBusinessService));
            services.AddTransient(typeof(ArticleBusinessService));

            services.AddTransient(typeof(PreInscriptionSousTraitenceBusinessService));

            services.AddHttpClient();
            services.AddScoped<IRecaptchaService,RecaptchaService>();


            services.Configure<LocalizationOptions>(options =>
            {
                options.ResourcesPath = "Resources";
            });

            var cultures = new List<CultureInfo>
            {
                new CultureInfo("ar-DZ"),
                new CultureInfo("fr"),
                new CultureInfo("en")
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var defaultCulture = new CultureInfo("ar-DZ");

                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);

                options.RequestCultureProviders.Clear();

                options.RequestCultureProviders.Add(new CustomRequestCultureProvider(context =>
                {
                    return Task.FromResult(new ProviderCultureResult("ar-DZ", "ar-DZ"));
                }));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<RequestLocalizationOptions> locOptions)
        {
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
