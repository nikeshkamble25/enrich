using EnrichMyCare.EnrichDatabase.Entities;
using EnrichMyCare.EnrichMyCareService.Infrastructure;
using EnrichMyCare.Repositories.Contracts;
using EnrichMyCare.Repositories.Infrastructure;
using EnrichMyCare.Repositories.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using NLog;
using System;
using System.IO;

namespace EnrichMyCareService
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup method
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }

        /// <summary>
        /// Configuration Property
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Added CORS settings for different domain API to consume in Client scripting projects (Angular/React etc)
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options => 
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            
            //Adding the EntityFramework DbContext Depedency
            services.AddDbContext<EnrichMyCareContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #region Injecting Repository Dependencies

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<IPatientProgramRepository, PatientProgramRepository>();
            services.AddScoped<IExcerciseRepository, ExcerciseRepository>();
            services.AddScoped<IImageVideoRepository, ImageVideoRepository>();
            services.AddScoped<IInitialProgressionRepository, InitialProgressionRepository>();
            services.AddScoped<IProgressionRepository, ProgressionRepository>();

            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "EnrichMyCare API Catalog",
                    Version = "v2",
                    Description = "EnrichMyCare API Catalog",
                });

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "EnrichMyCareAPI.xml");
                options.IncludeXmlComments(filePath);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), @"Upload\\files")),
                RequestPath = new PathString("/images")
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "EnrichMyCare API Catalog"));
        }
    }
}
