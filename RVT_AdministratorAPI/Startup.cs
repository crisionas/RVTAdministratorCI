using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RVT_AdministratorAPI.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RVT_AdministratorAPI
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
            services.AddSingleton(opt =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["QueueHost"],
                    UserName = Configuration["RabbitMQUsername"],
                    Password = Configuration["RabbitMQPassword"],
                    Port = 5672
                };
                return new RabbitMQQueueConnection(factory);
            });
            services.AddControllers();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseRabbitListener();
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static RabbitMQQueueConnection Listener { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder application)
        
       {
            Listener = application.ApplicationServices.GetService<RabbitMQQueueConnection>();

            var lifetime = application.ApplicationServices.GetService<IHostApplicationLifetime>();
            lifetime.ApplicationStarted.Register(OnStarted);
            lifetime.ApplicationStopping.Register(OnStoped);
            return application;
        }

        private static void OnStoped()
        {
            Listener.Disconnect();
        }

        private static void OnStarted()
        {
            Listener.InitReceiverChannel();
        }
    }
}
