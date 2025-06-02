using BlockedCountryAPI.Extensions;
using Contracts;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(op => op.SuppressModelStateInvalidFilter = true);

builder.Services.AddSingleton<IBlockedCountryRepository, BlockedCountryRepository>();

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