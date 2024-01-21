using System;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.OpenApi.Models;
using ExpensePaymentSystem.Business;
using MediatR;
using ExpensePaymentSystem.Api.Middleware;
using ExpensePaymentSystem.Base.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ExpensePaymentSystem.Business.Services;
using ExpensePaymentSystem.Business.Configuration;
using ExpensePaymentSystem.Business.Mapper;
using ExpensePaymentSystem.Data;

namespace ExpensePaymentSystem.Api
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<ExpensePaymentSystemDbContext>(options => options.UseSqlServer(connection));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).GetTypeInfo().Assembly));

            services.AddScoped<ReportService>();

            services.AddHttpClient<PaymentSimulationService>();

            services.AddHostedService<PaymentSimulationBackgroundService>();
            services.AddScoped<PaymentSimulationBackgroundService>();

            services.AddControllers().AddFluentValidation(x =>
                x.RegisterValidatorsFromAssemblyContaining<AssemblyReference>());

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));
            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExpensePaymentSystem API", Version = "v1.0" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Vb Management for IT Company",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, Array.Empty<string>() }
                });
            });

            JwtConfig jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
                    ValidAudience = jwtConfig.Audience,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExpensePaymentSystem v1");
                });
            }
            app.UseMiddleware<HeartBeatMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            var backgroundService = app.ApplicationServices.GetRequiredService<PaymentSimulationBackgroundService>();
            Task.Run(() => backgroundService.StartAsync(default)); // CancellationToken.None de kullanılabilir
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(x => { x.MapControllers(); });
        }
    }
}
