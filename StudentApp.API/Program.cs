using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentApp.Business.DependencyResolvers.Autofac;
using StudentApp.Core.Utilities.Security.JWT;
using StudentApp.DataAccessLayer.Concrete;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();

builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddSingleton<ITokenHelper, JwtHelper>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Bu ayarlar token'�n nas�l do�rulanaca��n� s�yler
            ValidateIssuer = true, // Issuer'� (vereni) do�rula
            ValidateAudience = true, // Audience'� (kullan�c�y�) do�rula
            ValidateLifetime = true, // Token'�n s�resini (�mr�n�) do�rula
            ValidateIssuerSigningKey = true, // Gizli anahtar� do�rula

            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
            RoleClaimType = ClaimTypes.Role
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("Authentication failed: " + context.Exception.Message, context.Request.Headers["Authorization"]);
                // Buraya breakpoint koyup context.Exception'� incele
                Console.WriteLine( context.Request.Headers["Authorization"]);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated successfully for: " + context.Principal.Identity.Name);
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Swagger UI i�in ba�l�k ve versiyon bilgisi
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "��renci Not Sistemi API", Version = "v1" });

    // 1. "Authorize" Butonunu Ekleyen Tan�mlama
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "L�tfen 'Bearer ' (bo�luk) ve ard�ndan token'�n�z� girin. \n\n�rnek: 'Bearer eyJhbGciOiJIEnc...",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http, // BU 'Http' OLMALI
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // 2. Kilit Simgelerini Ekleyen Gereksinim
    // �NCEK� KARI�IK BLOK YER�NE BUNU KULLANIN:
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                // Yukar�da 'Bearer' ad�yla tan�mlad���m�z �emaya referans veriyoruz
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" // Bu 'Id', AddSecurityDefinition'daki 'Bearer' ile e�le�meli
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
