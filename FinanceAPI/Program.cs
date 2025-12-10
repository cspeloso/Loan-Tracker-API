using FinanceAPI.Data;
using FinanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //  register DB Context 
        builder.Services.AddDbContext<FinanceDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("default")));

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        var app = builder.Build();

        app.UseCors();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.Run();
    }
}