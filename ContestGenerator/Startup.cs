using ContestGenerator.Abstractions;
using ContestGenerator.Data;
using ContestGenerator.Impls;
using ContestGenerator.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContestGenerator
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["CONNECTION_STRING"] ?? throw new InvalidOperationException("connection string not found");

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddScoped<ICaddyApi, CaddyApi>();
            services.AddScoped<IExcelRepo, ExcelRepo>();
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // Enables immediate logout, after updating the user's security stamp.
                options.ValidationInterval = TimeSpan.Zero;
            });
            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/users/login";
                options.AccessDeniedPath = "/";
            });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallback(async context =>
                {
                    context.Response.Redirect("/");
                }); // Redirect not found maps to home page
            });
        }



    }
}
