namespace FinanceAPI
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
                app.MapOpenApi();

            // app.UseHttpsRedirection();

            app.MapGet("/loans", () =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
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
                return forecast;
            })
            .WithName("GetLoans");



            app.Run();
        }
    }
}



public record CreateLoanRequest(
    string Name, 
    decimal Principal, 
    decimal InterestRate,
    int TermInMonths,
    decimal MonthlyPayment,
    DateTime StartDate
);

public record CreatePaymentRequest(decimal Amount, DateTime PaymentDate);