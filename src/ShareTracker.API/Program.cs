using Microsoft.EntityFrameworkCore;
using ShareTracker.Infrastructure.Persistence;
using ShareTracker.Application.Interfaces;
using ShareTracker.Application.Services;
using ShareTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShareTrackerDbContext>(options =>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<ISecurityRepository, SecurityRepository>();
builder.Services.AddScoped<IBrokerService, BrokerService>();
builder.Services.AddScoped<IBrokerRepository, BrokerRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
