
using Microsoft.EntityFrameworkCore;
using MobDeMob.Infrastructure;
using MobDeMob.Application;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// .AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
// });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGenNewtonsoftSupport();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(p => p.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    }));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(MyAllowSpecificOrigins);

    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await initializer.SeedAsync();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
