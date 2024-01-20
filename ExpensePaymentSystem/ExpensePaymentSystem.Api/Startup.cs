
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ExpensePaymentSystem.Data;
using AutoMapper;
using ExpensePaymentSystem.Business.Mapper;
using Microsoft.OpenApi.Models;
using ExpensePaymentSystem.Business;
using MediatR;

namespace ExpensePaymentSystem.Api;

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
        //services.AddDbContext<VbDbContext>(options => options.UseNpgsql(connection));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).GetTypeInfo().Assembly));

        //var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));
        //services.AddAutoMapper(typeof(AssemblyReference).GetTypeInfo().Assembly);

        services.AddControllers().AddFluentValidation(x =>
            x.RegisterValidatorsFromAssemblyContaining<AssemblyReference>());


        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));
        services.AddSingleton(mapperConfig.CreateMapper());

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExpensePaymentSystem", Version = "v1" });
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

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(x => { x.MapControllers(); });
    }
}