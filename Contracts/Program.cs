using Contracts.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddService();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(InstallServices).Assembly));

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();


app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
