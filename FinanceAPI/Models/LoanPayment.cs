namespace FinanceAPI.Models;

public class LoanPayment
{
    public int Id { get; set; }
    public int LoanID { get; set; }
    public Loan Loan { get; set; }

    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public decimal PrincipalApplied { get; set; }
    public decimal InterestApplied { get; set; }
}
