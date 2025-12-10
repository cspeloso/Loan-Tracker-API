namespace FinanceAPI.Controllers;

using FinanceAPI.Models;
using FinanceAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("loans")]
public class LoansController : ControllerBase
{
    private readonly FinanceDbContext _db;

    public LoansController(FinanceDbContext db) => _db = db;

    #region POSTs
    [HttpPost]
    public async Task<IActionResult> CreateLoan(CreateLoanRequest request)
    {
        var loan = new Loan
        {
            Name = request.Name,
            Principal = request.Principal,
            InterestRate = request.InterestRate,
            TermInMonths = request.TermInMonths,
            MonthlyPayment = request.MonthlyPayment,
            StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc)
        };

        _db.Loans.Add(loan);

        await _db.SaveChangesAsync();

        return Created($"/loans/{loan.Id}", loan);
    }
    #endregion POSTs

    #region GETs
    [HttpGet]
    public async Task<List<Loan>> GetLoans() => await _db.Loans.ToListAsync();
    #endregion GETs

}