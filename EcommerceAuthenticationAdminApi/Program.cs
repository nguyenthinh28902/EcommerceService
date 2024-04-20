
using EcommerceAuthenticationAdminApi.Helpers;
using EcommerceAuthenticationService.AutoMapper;
using EcommerceAuthenticationService.DependencyInjection;
using EcommerceAuthenticationService.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog.Formatting.Compact;
using Serilog;
using System.Text;
using Serilog.Events;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
//.WriteTo.File(new CompactJsonFormatter(), "logs\\log-.log", shared: true, rollingInterval: RollingInterval.Day)

// Khởi tạo logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}", restrictedToMinimumLevel: LogEventLevel.Debug)
    .WriteTo.File( "logs\\log-.log", 
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {SourceContext} - {Message}{NewLine}", 
    rollingInterval: RollingInterval.Day, 
    restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<BlacklistAuthorizationFilter>(); // Thêm bộ lọc kiểm tra blacklist vào mọi API
});
builder.Services.AddAuthorization();
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

var test = builder.Configuration["JwtSettings:Issuer"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Aud"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"] ?? ""))
        };
    });
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce Authentication Api", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.OperationFilter<AuthorizationOperationFilter>();
});

builder.Services.ServiceAutoMapper(builder.Configuration);
builder.Services.ServiceDescriptors(builder.Configuration);
var corsConfiguration = builder.Configuration.GetSection("WithOrigins").Get<List<string>>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.DisplayRequestDuration();

    });
}

app.UseCors(x => x
       .WithOrigins(corsConfiguration.ToArray())
       .AllowAnyMethod()
       .AllowAnyHeader());

app.Services.ServiceProvider();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
