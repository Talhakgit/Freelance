using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();



builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
}
);





builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));



builder.Services.AddCors(p => p.AddPolicy("AllowOrigin", builder =>

{

    builder.WithOrigins("https://localhost:7171").AllowAnyMethod().AllowAnyHeader();
    builder.WithOrigins("https://localhost:7267").AllowAnyMethod().AllowAnyHeader();
    builder.WithOrigins("http://localhost:7267").AllowAnyMethod().AllowAnyHeader();

}));

var Configuration = builder.Configuration;
    builder.Services.AddDependencyResolvers(new Core.Utilities.IoC.ICoreModule[]
      {
        new CoreModule()
      });



    var app = builder.Build();
  




    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())

    {

        app.UseSwagger();

        app.UseSwaggerUI();

    }



    app.UseCors("AllowOrigin");

    app.UseHttpsRedirection();



    app.UseAuthorization();

    app.UseAuthentication();

    app.MapControllers();



    app.Run();
