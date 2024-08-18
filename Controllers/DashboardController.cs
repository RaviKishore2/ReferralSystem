using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Referral_System.DTO;

namespace Referral_System.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly ReferralSystemDbContext _context;

        public DashboardController(ReferralSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> ViewReferralStatus(int userId)
        {
            var sucessful = await _context.Referrals.CountAsync(r => r.ReferrerId == userId && r.Status == "Completed!");
            var ongoing = await _context.Referrals.CountAsync(r => r.ReferrerId == userId && r.Status == "Pending");
            var totalReferrals = sucessful + ongoing;

            var statusDto = new ReferralStatusDto
            {
                TotalReferrals = totalReferrals,
                Successful = sucessful,
                Ongoing = ongoing
            };

            return Ok(statusDto);
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
