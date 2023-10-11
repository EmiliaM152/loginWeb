using fsw.web.Repositories;
using Microsoft.EntityFrameworkCore;
namespace fsw.web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {  Configuration = configuration; }

        public void ConfirgureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")),
                ServiceLifetime.Transient);

            services.AddCors(options =>
            {
                var frontendUrl = Configuration.GetValue<string>("UrlFrontend");
                options.AddDefaultPolicy(builder =>
                {
                    builder
                    .WithOrigins(frontendUrl)   
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env)
        {
            

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
               
        }
    }
}
