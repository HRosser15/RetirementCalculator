namespace RetirementCalculator.API.DTOs
{
    public class YearlyProjectionDto
    {
        public int Year { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public decimal ContributionAmount { get; set; }
        public decimal EmployerMatchAmount { get; set; }
        public decimal InvestmentReturns { get; set; }
        public decimal TotalContribution { get; set; }
        public decimal YearEndBalance { get; set; }
        public decimal InflationAdjustedBalance { get; set; }
        public decimal InflationAdjustedSalary { get; set; }
    }
}
