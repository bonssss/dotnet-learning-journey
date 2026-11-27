using System.IO;
using System.Reflection;
using System.Text;
using AddsEats.Api.Configurarions;
using AddsEats.Api.Filters;
using AddsEats.Api.Middleware;
using AddsEats.Api.Models;
using AddsEats.Api.Services;
using AddsEats.Api.Services.Diagnostics;
using AddsEats.Api.Services.MenuItemServices;
using AddsEats.Api.Services.OrderServices;
using AddsEats.Api.Services.RestaurantServices;
using AddsEats.Api.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // Exercise 7: Global audit filter logging action name and bound arguments
    options.Filters.Add<AuditActionFilter>();
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IScopedStateService, ScopedStateService>();
builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();
builder.Services.AddScoped<IRestaurantServices, RestaurantServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<AuthService>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<DemoUserServices>();

var jwtSigningKey = builder.Configuration["Jwt:SigningKey"]
    ?? builder.Configuration["Jwt:Signingkey"]
    ?? "AddsEats-Development-Signing-Key-Change-Me-1234567890";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "addiseats-api",
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? "addiseats-client",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSigningKey))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AddsEats API",
        Version = "v1",
        Description = "REST API for discovering Ethiopian restaurants, browsing menus (with VAT), placing orders, and authenticating demo users.",
        Contact = new OpenApiContact
        {
            Name = "AddsEats Team"
        },
        License = new OpenApiLicense
        {
            Name = "MIT"
        }
    });

    // XML comments from controllers and DTOs are rendered as summaries in Swagger UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT access token from POST /api/auth/login. Enter only the token, without the 'Bearer ' prefix."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document),
            new List<string>()
        }
    });
});

var app = builder.Build();

// Middleware pipeline (Exercise 1, 2, 3)
app.UseCorrelationId();
app.UseRequestTiming();
app.UseScopedServiceExperiment();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AddsEats API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Exercise 4: ApiKeyMiddleware applied strictly to the /admin branch via app.Map
app.Map("/admin", adminApp =>
{
    adminApp.UseMiddleware<ApiKeyMiddleware>();

    adminApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            status = "Success",
            message = "Welcome to the secure AddsEats Admin area.",
            serverTimeUtc = DateTime.UtcNow
        });
    });
});

app.MapControllers();

app.MapGet("/api/diagnostics/slow", async () =>
{
    await Task.Delay(1050);
    return Results.Ok(new { message = "Simulated slow response" });
});

app.Run();
