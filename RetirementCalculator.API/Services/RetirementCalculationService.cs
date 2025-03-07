using RetirementCalculator.API.DTOs;
using RetirementCalculator.API.Models;

namespace RetirementCalculator.API.Services
{
    public class RetirementCalculationService
    {
        public List<YearlyProjectionDto> CalculateProjections(RetirementScenario scenario, UserProfile profile)
        {
            var projections = new List<YearlyProjectionDto>();

            decimal currentBalance = profile.CurrentBalance;
            decimal currentSalary = profile.CurrentSalary;
            int currentAge = profile.CurrentAge;
            int retirementAge = profile.RetirementAge;

            decimal inflationFactor = 1.0m;

            for (int age = currentAge; age <= retirementAge; age++)
            {
                int year = DateTime.Now.Year + (age - currentAge);
                decimal contributionAmount = currentSalary * (scenario.ContributionPercentage / 100m);
                decimal employerMatchAmount = 0;
                
                if (profile.EmployerMatchPercentage.HasValue)
                {
                    decimal matchablePercentage = Math.Min(
                        scenario.ContributionPercentage,
                        profile.EmployerMatchLimit ?? decimal.MaxValue
                    );
                    
                    employerMatchAmount = currentSalary * (matchablePercentage / 100m) * profile.EmployerMatchPercentage.Value / 100m;
                }

                decimal totalContribution = contributionAmount + employerMatchAmount;

                // annual return - inflation  
                decimal realReturnRate = (1 + scenario.AnnualReturnRate / 100m) / (1 + scenario.Inflation / 100m) - 1;
                decimal investmentReturn = currentBalance * realReturnRate;

                decimal newBalance = currentBalance + investmentReturn + totalContribution;
                decimal inflationAdjustedBalance = newBalance / inflationFactor;
                decimal inflationAdjustedSalary = currentSalary / inflationFactor;

                var yearlyProjection = new YearlyProjectionDto
                {
                    Year = year,
                    Age = age,
                    Salary = currentSalary,
                    ContributionAmount = contributionAmount,
                    EmployerMatchAmount = employerMatchAmount,
                    InvestmentReturns = investmentReturn,
                    TotalContribution = totalContribution,
                    YearEndBalance = newBalance,
                    InflationAdjustedBalance = inflationAdjustedBalance,
                    InflationAdjustedSalary = inflationAdjustedSalary
                };

                projections.Add(yearlyProjection);

                // Update values for next year
                currentBalance = newBalance;

                // Apply salary growth for next year
                // You could use nominal growth or real salary growth
                currentSalary = currentSalary * (1 + scenario.SalaryGrowthRate / 100m);

                // Update inflation factor for next year
                inflationFactor *= (1 + scenario.Inflation / 100m);

            }

                return projections;
        }
    }
}
