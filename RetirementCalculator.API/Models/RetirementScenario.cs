namespace RetirementCalculator.API.Models
{
    public class RetirementScenario
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Default";
        public int UserProfileId { get; set; }
        public decimal ContributionPercentage { get; set; }
        public decimal AnnualReturnRate { get; set; } = 10m;
        public decimal Inflation { get; set; } = 3m;
        public decimal SalaryGrowthRate { get; set; } = 3m;

        public UserProfile UserProfile { get; set; }
    }
}
