using dotNet_backend.Auth;
using dotNet_backend.Exceptions.GlobalExceptionHandler;
using dotNet_backend.Helpers;
using dotNet_backend.Helpers.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

void ConfigureServices(WebApplicationBuilder builderInstance)
{
    var configuration = builderInstance.Configuration;
    Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
        .WriteTo.File("log/KarateLogs.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
    builderInstance.Services.AddControllers().AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    builderInstance.Host.UseSerilog();
    builderInstance.Services.AddAutoMapper(typeof(MapperProfile));
    builderInstance.Services.AddControllers();
    builderInstance.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    builderInstance.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    builderInstance.Services.AddAuthentications(configuration);
    builderInstance.Services.AddRepositories();
    builderInstance.Services.AddServices();
    builderInstance.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builderInstance.Services.AddSwagger();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builderInstance.Services.AddEndpointsApiExplorer();

    builderInstance.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
    });

    var app = builderInstance.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCors("AllowAll");

    app.UseAuthentication();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseExceptionHandler(_ => { });

    app.Run();
}

ConfigureServices(builder);