using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model;

namespace GuestBook
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
            services.AddDbContext<GuestBookDBContext>();
            services.AddSwaggerGen();
            services.AddMvc();
            services.AddControllers(options => options.EnableEndpointRouting = false);

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host("amqp://guest:guest@localhost:5672");

                });
            });

            services.AddMassTransitHostedService();
            

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseHttpsRedirection();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            //var bus = Bus.Factory.CreateUsingRabbitMq(config =>
            //{
            //    config.Host("amqp://guest:guest@localhost:5672");
            //    config.ReceiveEndpoint("temp-queue", c =>
            //    {
            //        c.Handler<Comment>(ctx =>
            //        {
            //            return Console.Out.WriteLineAsync(ctx.Message.Name);

            //        });


            //    });
            //});
            //bus.Start();
            //bus.Publish(new Comment { Name = "Test name" });
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Heh API V1"); });
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{Id?}"
                    );
            });
            

        }
    }
}
