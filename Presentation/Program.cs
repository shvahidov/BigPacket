using System.Text;
using Application.Interfaces;
using Application.Validators;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence;

// using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// БД
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Сервисы
// builder.Services.AddScoped<IAccountService, AccountService>();

// Контроллеры
// builder.Services.AddControllers();
#pragma warning disable CS0618 // Type or member is obsolete
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
#pragma warning disable CS0618 // Type or member is obsolete
        fv.RegisterValidatorsFromAssemblyContaining<AccountValidator>());
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
#pragma warning disable CS0618 // Type or member is obsolete
        fv.RegisterValidatorsFromAssemblyContaining<TransferRequestValidator>();
#pragma warning restore CS0618 // Type or member is obsolete
    });
#pragma warning restore CS0618 // Type or member is obsolete
// Подключаем Swagger + Авторизацию
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Bank API", Version = "v1" });

    // Настраиваем JWT авторизацию в Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Введите JWT токен с префиксом 'Bearer '",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank API v1");
    c.RoutePrefix = string.Empty; // 👉 теперь Swagger открывается сразу на https://localhost:5001/
});

// Подключаем Middleware
app.UseHttpsRedirection();

app.UseAuthentication(); // проверка токена
app.UseAuthorization();  // проверка прав

app.MapControllers();

app.Run();
