using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacilityApi.Commands;
using FacilityApi.Cqrs;
using FacilityApi.Events;
using FacilityApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FacilityApi
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
            services.AddSingleton<ICommandBus, FakeBus>();
            services.AddSingleton<FacilityCommandHandlers>();
            services.AddSingleton<IEventStore, EventStore>();
            services.AddSingleton<IRepository<Facility>, EventSourcedRepository<Facility>>();

            // TODO: Remove when using an external event publisher, like Kafka
            services.AddSingleton<IEventPublisher, FakeBus>();
            
            services.AddMvc();
        }

        // This method gets called by the runtime, but was manually introduced.
        // Use this method to configure injected services.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var env = serviceProvider.GetService<IHostingEnvironment>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            var commandBus = serviceProvider.GetService<ICommandBus>() as FakeBus;
            if(commandBus != null)
            {
                var facilityCommandHandlers = serviceProvider.GetService<FacilityCommandHandlers>();
                commandBus.RegisterHandler<CreateFacilityCommand>(facilityCommandHandlers.Handle);
            }

            var eventPublisher = serviceProvider.GetService<IEventPublisher>() as FakeBus;
            if(eventPublisher != null)
            {
                eventPublisher.RegisterHandler<FacilityCreatedEvent>((e) => {
                    System.Console.WriteLine($"Created facility({e.FacilityId} - {e.FacilityName})");
                });
            }
        }
    }
}
