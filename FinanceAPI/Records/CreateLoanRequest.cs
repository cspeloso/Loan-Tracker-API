public record CreateLoanRequest(
    string Name, 
    decimal Principal, 
    decimal InterestRate,
    int TermInMonths,
    decimal MonthlyPayment,
    DateTime StartDate
);