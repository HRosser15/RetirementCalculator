using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RetirementCalculator.API.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentAge { get; set; }
        public int RetirementAge { get; set; }
        public decimal CurrentSalary { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal ContributionPercentage { get; set; }
        public decimal? EmployerMatchPercentage { get; set; }
        public decimal? EmployerMatchLimit { get; set; }

        public List<RetirementScenario> Scenarios { get; set; }
    }

}
