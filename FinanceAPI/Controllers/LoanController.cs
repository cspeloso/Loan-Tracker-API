namespace FinanceAPI.Controllers;

using FinanceAPI.Models;
using FinanceAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Loan>> GetLoan(int id)
    {
        var loan = await _db.Loans.FindAsync(id);
        return loan is null ? NotFound() : Ok(loan);
    }
    #endregion GETs

    #region PUTs
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLoan(int id, [FromBody] UpdateLoanRequest request)
    {
        var startUtc = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);

        var rows = await _db.Loans
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.Name, request.Name)
                .SetProperty(x => x.Principal, request.Principal)
                .SetProperty(x => x.InterestRate, request.InterestRate)
                .SetProperty(x => x.TermInMonths, request.TermInMonths)
                .SetProperty(x => x.MonthlyPayment, request.MonthlyPayment)
                .SetProperty(x => x.StartDate, startUtc)
            );

        return rows == 0 ? NotFound() : NoContent();
    }
    #endregion

    #region DELETEs
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        var rows = await _db.Loans.Where(x => x.Id == id).ExecuteDeleteAsync();

        return rows == 0 ? NotFound() : NoContent();
    }
    #endregion


    public sealed class UpdateLoanRequest
    {
        public string Name { get; set; } = "";
        public decimal Principal { get; set; }
        public decimal InterestRate { get; set; }
        public int TermInMonths { get; set; }
        public decimal MonthlyPayment { get; set; }
        public DateTime StartDate { get; set; }
    }
}