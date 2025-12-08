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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();

        app.MapGet("/loans", async (FinanceDbContext db) =>
        {
            return Results.Ok(await db.Loans.ToListAsync());
        });


        app.MapPost("/loans", async (CreateLoanRequest request, FinanceDbContext db) =>
        {
            var loan = new Loan
            {
                Name = request.Name,
                Principal = request.Principal,
                InterestRate = request.InterestRate,
                TermInMonths = request.TermInMonths,
                MonthlyPayment = request.MonthlyPayment,
                StartDate = request.StartDate
            };

            db.Loans.Add(loan);

            await db.SaveChangesAsync();

            return Results.Created($"/loans/{loan.Id}", loan);
        });


        app.Run();
    }
}