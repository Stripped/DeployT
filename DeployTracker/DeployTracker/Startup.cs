using DeployTracker.Database;
using DeployTracker.Handler;
using DeployTracker.Options;
using DeployTracker.Services;
using DeployTracker.Services.Concrete;
using DeployTracker.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;


namespace DeployTracker
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddMvc();
            services.AddLogging();
            services.AddSingleton<ICounter, Counter>();
            services.AddScoped<IMathService, MathService>();           
            services.AddScoped<IAuthOptions, AuthOptions>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<ILoginJWT, LoginJWT>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddOptions();
            services.Configure<EmailServiceOptions>(Configuration.GetSection(nameof(EmailServiceOptions))); 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "MyScheme";
            })
                .AddScheme<AuthenticationSchemeOptions, AuthHandler>("MyScheme", null);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });        
        }
    }
}
