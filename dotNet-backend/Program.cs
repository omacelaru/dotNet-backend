using dotNet_backend.Auth;
using dotNet_backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

void ConfigureServices(WebApplicationBuilder builderInstance)
{

    var configuration = builderInstance.Configuration;
    builderInstance.Services.AddControllers();
    builderInstance.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    builderInstance.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    builderInstance.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };
    });

    builderInstance.Services.AddScoped<AuthService>();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builderInstance.Services.AddEndpointsApiExplorer();
    builderInstance.Services.AddSwaggerGen();



    var app = builderInstance.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

ConfigureServices(builder);
