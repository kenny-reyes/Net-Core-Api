﻿using System;
using ApiExercise.Api;
using ApiExercise.Host.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ApiExercise.Host
{
    public class Startup
    {
        private readonly string _allowedOrigins = "_AllowedOrigins";

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services, Configuration, Environment)
                .AddDistributedMemoryCache()
                //.AddOpenApi()
                .AddCors(options =>
                {
                    // better in appsettings
                    options.AddPolicy("AllowAllHeaders",
                        corsbuilder =>
                        {
                            corsbuilder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .WithOrigins("http://localhost:3000");
                        });
                })
                .AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
                
                services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("AllowAllHeaders");
            }
            else
            {
                app.UseCors(cors => cors
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());
            }

            ApiConfiguration.Configure(app, env)
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
        }
    }
}