using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetirementCalculator.API.Data;
using RetirementCalculator.API.DTOs;
using RetirementCalculator.API.Models;
using RetirementCalculator.API.Services;

namespace RetirementCalculator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetirementController : ControllerBase
    {
        private readonly RetirementContext _context;
        private readonly RetirementCalculationService _calculationService;

        public RetirementController(RetirementContext context, RetirementCalculationService calculationService)
        {
            _context = context;
            _calculationService = calculationService;
        }

        [HttpPost("calculate")]
        public ActionResult<List<YearlyProjectionDto>> CalculateProjections(RetirementScenario scenario, UserProfile profile)
        {
            if (scenario == null || profile == null)
            {
                return BadRequest("Scenario and profile are required");
            }

            var projections = _calculationService.CalculateProjections(scenario, profile);
            return Ok(projections);
        }

        [HttpPost("profiles")]
        public ActionResult<UserProfile> CreateProfile(UserProfile profile)
        {
            if (profile == null)
            {
                return BadRequest("Profile data is required");
            }

            if (string.IsNullOrWhiteSpace(profile.Name))
            {
                return BadRequest("Name is required");
            }

            if (profile.CurrentAge <= 0 || profile.RetirementAge <= 0)
            {
                return BadRequest("Age values must be positive");
            }

            if (profile.RetirementAge < = profile.CurrentAge)
            {
                return BadRequest("Retirement age must be greater than current age");
            }

            if (profile.CurrentSalary < 0)
            {
                return BadRequest("Salary cannot be negative");
            }

            if (profile.ContributionPercentage < 0 || profile.ContributionPercentage > 100)
            {
                return BadRequest("Contribution percentage must be between 0 and 100");
            }

            try
            {
                // initialize list if null
                if (profile.Scenarios == null)
                {
                    profile.Scenarios = new List<RetirementScenario>();
                }

                // add to DB
                _context.UserProfiles.Add(profile);
                _context.SaveChanges();

                // return 201 created with the location header and created object
                // READ UP ON THIS BLOCK
                return CreatedAtAction(
                    nameof(GetProfile),
                    new { id = profile.Id },
                    profile
                );

            }
            catch (Exception ex) 
            {
                return StatusCode(500, "An error occurred while creating the profile");
            }


        }

        [HttpGet("profiles/{id}")]
        public ActionResult<UserProfile> GetProfile(int id)
        {
            var profile = _context.UserProfiles.Include(p => p.Scenarios).FirstOrDefault(p => p.Id == id);

            if (profile == null)
            {
                return NotFound();
            }

            return profile;
        }

        //[HttpPost("profiles/{id}/scenarios")]
        //public ActionResult<RetirementScenario> AddScenario(int id, RetirementScenario scenario)
        //{

        //}

        //[HttpGet("profiles/{profileId}/scenarios/{scenarioId}/projections")]
        //public ActionResult<List<YearlyProjectionDto>> GetProjections(int profileId, int scenarioId)
        //{

        //}
    }
}