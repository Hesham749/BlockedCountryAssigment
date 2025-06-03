using BlockedCountryAPI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(op => op.SuppressModelStateInvalidFilter = true);

builder.Services.Configure<GeoApiOptions>(builder.Configuration.GetSection("GeoApi"));

builder.Services.AddServices();

builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

app.AddConfigureExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();