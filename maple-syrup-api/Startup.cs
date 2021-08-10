using maple_syrup_api.Context;
using maple_syrup_api.Helpers;
using maple_syrup_api.Repositories.IRepository;
using maple_syrup_api.Repositories.Repository;
using maple_syrup_api.Services.IService;
using maple_syrup_api.Services.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace maple_syrup_api
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

            AddServiceAndRepositories(services);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy( builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            services.AddControllers();

            services.AddDbContext<MapleSyrupContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "maple_syrup_api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "maple_syrup_api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseMiddleware<AuthenticationMiddleWare>();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private void AddServiceAndRepositories(IServiceCollection services) {

            //Config
            services.AddSingleton(Configuration);


            // JWT
            services.AddTransient<IJwtUtils, JwtUtils>();

            //User
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserTokenRepository, UserTokenRepository>();

            //Event
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IEventService, EventService>();

            //GuildConfig
            services.AddTransient<IGuildConfigRepository, GuildConfigRepository>();
            services.AddTransient<IGuildConfigService, GuildConfigService>();

        }
    }
}
