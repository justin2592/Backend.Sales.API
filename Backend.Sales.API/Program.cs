using Backend.Sales.Application.Constants;
using Backend.Sales.Application.Installers;
using Backend.Sales.Application.Services.MiddleWare;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Injecting services.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Servicing API", Version = "v1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API key access",
        Type = SecuritySchemeType.ApiKey,
        Name = HttpConstants.API_KEY,
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });
    var scheme1 = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
                {
                    { scheme1, new List<string>() { "ApiKey" } }
                };
    c.AddSecurityRequirement(requirement);
    c.UseAllOfToExtendReferenceSchemas();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();