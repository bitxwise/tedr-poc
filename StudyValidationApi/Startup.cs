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
using Risly.Cqrs;
using Risly.Cqrs.Kafka;
using StudyValidationApi.Events;

namespace StudyValidationApi
{
    public class Startup
    {
        private KafkaEventConsumer _studyKafkaEventConsumer;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // publishes events to other services
            services.AddSingleton<IEventPublisher, KafkaEventPublisher>();
            
            // handles events published by other services
            services.AddSingleton<IEventHandler, StudyEventHandler>();
            services.AddSingleton<KafkaEventConsumer>();

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

            string topicName = "studies";
            var eventPublisher = serviceProvider.GetService<IEventPublisher>() as KafkaEventPublisher;
            eventPublisher.TopicName = topicName;
            
            _studyKafkaEventConsumer = serviceProvider.GetService<KafkaEventConsumer>();
            _studyKafkaEventConsumer.TopicName = topicName;
            _studyKafkaEventConsumer.Start();
        }

        private void OnShutdown()
        {
            if(!_studyKafkaEventConsumer.IsStopping)
                _studyKafkaEventConsumer.Stop();
        }
    }
}
