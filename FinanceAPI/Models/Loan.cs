namespace FinanceAPI.Models;

public class Loan
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Principal { get; set; }
    public decimal InterestRate { get; set; }
    public int TermInMonths { get; set; }
    public DateTime StartDate { get; set; }
    public decimal MonthlyPayment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<LoanPayment> Payments { get; set; } = null;
}
