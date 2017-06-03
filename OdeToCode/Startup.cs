using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using OdeToCode.Services;

namespace OdeToCode
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //For AddSingleton services there is only ONE instance of services throughout the app.
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter, Greeter>();

            //scoped services are instanciated every time with a new HTTP request.
            services.AddScoped<IRestaurantData, InMemoryRestaurantData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //order of the middleware is important.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IGreeter greeter)
        {
            //this middleware is logging the request to console.
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                //if any unhandled exception is there that will be handled by this middleware.
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("Opps!")
                });
            }

            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            //this middleware provide functionality of both UserDefaultFiles and UseStatcticFiles
            app.UseFileServer();


            //app.UseMvcWithDefaultRoute();

            app.UseMvc(route =>
                route.MapRoute("Default", "{Controller=Home}/{Action=Index}/{id?}")
            );

            app.Run(ctx => ctx.Response.WriteAsync("Opps!!"));

            /*
            //this middleware will respond to "/Welcome" route .any other route will be routed to RUN middleware.
            //we can configure any middleware with options object.
            app.UseWelcomePage(new WelcomePageOptions
            {
                Path = "/Welcome"
            });

            app.Run(async (context) =>
            {
                //throw new System.Exception("Error");

                //getting greeting from a configurable service.
                var message = greeter.GetGreeting();
                await context.Response.WriteAsync(message + "\n");

                //we can dig around in request pipeline using "Context" object.
                //await context.Response.WriteAsync(context.Request.Headers.ToString());
            });
            */
        }
    }
}
