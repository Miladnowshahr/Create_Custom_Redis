using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RedisVideo.Common.Abstraction;
using RedisVideo.Common.Implementation;
using RedisVideo.Master.Application;
using RedisVideo.Master.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisVideo.Master
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

            services.AddControllers();

            services.AddSwaggerGen();

            services.AddSingleton<IHashGenerate, JenkinsHashGenerator>()
                .AddSingleton<IBinarySerializer, JsonSerializer>()
                .AddSingleton<IBitHelper, BitHelper>()
                .AddSingleton<IPrimeNumberService, PrimeNumberService>()
                .AddSingleton<IMasterService, MasterService>()
                .AddHttpClient<IChildClient, ChildClient>();
            
            services.AddHttpClient<IMasterClient, MasterClient>();
            services.AddSingleton<IReplicationService, ReplicationService>();
            services.Configure<MasterOptions>(Configuration.GetSection(nameof(MasterOptions)));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
