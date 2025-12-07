using FinanceAPI.Models;

namespace FinanceAPI
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            app.MapGet("/loans", () =>
            {
                var currentLoans = Enumerable.Range(1, 5).Select(index =>
                    new Loan
                    {
                        Id = index,
                        Name = $"Loan {index}",
                        Principal = 15000,
                        InterestRate = 6.3M,
                        TermInMonths = 72,
                        StartDate = DateTime.Parse("7/1/2024"),
                        MonthlyPayment = 461
                    })
                    .ToArray();
                return currentLoans;
            })
            .WithName("GetLoans");

            
            app.MapPost("/loans", (CreateLoanRequest request) =>
            {
                var loan = new Loan
                {
                    Id = Random.Shared.Next(1000, 9999),
                    Name = request.Name,
                    Principal = request.Principal,
                    InterestRate = request.InterestRate,
                    TermInMonths = request.TermInMonths,
                    MonthlyPayment = request.MonthlyPayment,
                    StartDate = request.StartDate
                };

                return Results.Created($"/loans/{loan.Id}", loan);
            })
            .WithName("CreateLoan");


            app.Run();
        }
    }
}