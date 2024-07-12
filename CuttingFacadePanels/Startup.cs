using System.Text.Json.Serialization;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CuttingFacadePanels
{
	public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                var enumConverter = new JsonStringEnumConverter();
                options.JsonSerializerOptions.Converters.Add(enumConverter);
            });

            services.AddSwaggerGen();

            services.AddHealthChecks();
            services.AddMediatR(typeof(Startup));
            services.AddValidatorsFromAssemblyContaining<Polygon>();

            services.AddTransient<ICutService, CutService>();
            services.AddTransient<ICalculateAmountPanelsService, CalculateAmountPanelsService>();

            services.AddMediatR(typeof(Startup));
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", Program.ServiceName.Name));

            app.UseHealthChecks("/healthchecks");

            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthentication().UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}