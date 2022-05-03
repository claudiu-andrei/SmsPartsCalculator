using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SMSParts.Business.Implementation;
using SMSParts.Business.Implementation.Handlers;
using SMSParts.Business.Implementation.Handlers.NonStandard;
using SMSParts.Business.Implementation.Handlers.Standard;
using SMSParts.Business.Implementation.Helpers;
using SMSParts.Business.Interfaces;
using SMSParts.Business.Interfaces.Handlers;
using SMSParts.Business.Interfaces.Handlers.NonStandard;
using SMSParts.Business.Interfaces.Handlers.Standard;
using SMSParts.Business.Interfaces.Helpers;

namespace SMSParts.Web
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SMSParts", Version = "v1" });
            });

            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ISmsPartsService, SmsPartsService>();
            services.AddScoped<ITextToSmsPartsHandlerFactory, TextToSmsPartsHandlerFactory>();
            services.AddScoped<IMessageTypeCalculator, MessageTypeCalculator>();
            services.AddScoped<ICheckTextCharactersService, CheckTextCharactersService>();
            services.AddScoped<ISplitSmsService, SplitSmsService>();
            services.AddScoped<IValidateSmsInputText, ValidateSmsInputText>();

            AddHandlerServices(services);
            AddHelperServices(services);
        }

        private static void AddHandlerServices(IServiceCollection services)
        {
            services.AddScoped<IStandardGsmMultiPartSmsHandler, StandardGsmMultiPartSmsHandler>();
            services.AddScoped<IStandardGsmSinglePartSmsHandler, StandardGsmSinglePartSmsHandler>();

            services.AddScoped<INonStandardGsmMultiPartSmsHandler, NonStandardGsmMultiPartSmsHandler>();
            services.AddScoped<INonStandardGsmSinglePartSmsHandler, NonStandardGsmSinglePartSmsHandler>();
        }

        private static void AddHelperServices(IServiceCollection services)
        {
            services.AddScoped<IGsmCharactersBytesHelper, GsmCharactersBytesHelper>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SMSParts v1"));

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
