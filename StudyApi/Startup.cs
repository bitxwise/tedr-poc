using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Risly.Cqrs;
using Risly.Reflection;
using StudyApi.Commands;
using StudyApi.Events;
using StudyApi.Models;

namespace StudyApi
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
            services.AddSingleton<StudyCommandHandlers>();
            services.AddSingleton<IEventStore, EventStore>();
            services.AddSingleton<IRepository<Study>, EventSourcedRepository<Study>>();

            // TODO: Remove when using an external event publisher, like Kafka
            services.AddSingleton<IEventPublisher, FakeBus>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // However, this was commented out for a brute forced approach to tying events together
        // via Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        // public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        // {
        //     if (env.IsDevelopment())
        //     {
        //         app.UseDeveloperExceptionPage();
        //     }
        //
        //     app.UseMvc();
        // }

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
                var studyCommandHandlers = serviceProvider.GetService<StudyCommandHandlers>();
                commandBus.RegisterHandler<CreateStudyCommand>(studyCommandHandlers.Handle);
            }

            var eventPublisher = serviceProvider.GetService<IEventPublisher>() as FakeBus;
            if(eventPublisher != null)
            {
                eventPublisher.RegisterHandler<StudyCreatedEvent>((e) => {
                    System.Console.WriteLine($"Created study({e.StudyId})");

                    JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

                    var e_serialized = JsonConvert.SerializeObject(e, settings);

                    Event e_deserialized = JsonConvert.DeserializeObject<Event>(e_serialized, settings);

                    Study s = new Study();
                    s.AsDynamic().Apply(e_deserialized);

                    System.Console.WriteLine($"Serialized and then deserialized {s.Id}");
                });
            }
        }
    }
}
