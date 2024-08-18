using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Referral_System.DTO;
using Referral_System.Models;

namespace Referral_System.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RefferalController : Controller
    {

        private readonly ReferralSystemDbContext _context;

        public RefferalController(ReferralSystemDbContext context)
        {
            _context = context;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateReferral([FromBody] ReferralDto referralDto)
        {
            
                var referrer = await _context.Users.FindAsync(referralDto.ReferrerId);
                var referredUser = await _context.Users.FindAsync(referralDto.RefferedUserId);
                if (referrer == null || referredUser == null)
                {
                    return NotFound("User Not Found");
                }
                if (!referrer.EligibilityStatus)
                {
                    return BadRequest("Referrer is not eligible to refer others");
                }
                var referral = new Referral
                {
                    ReferrerId = referrer.UserID,
                    ReferredUserId = referredUser.UserID,
                    Status = "Pending",
                    Reward = 0
                };

                _context.Referrals.Add(referral);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Referral generated successfully" });
            
        }

        [HttpGet("track/{referralId}")]
        public async Task<IActionResult> Trackreferral(int referralId)
        {
            var referral = await _context.Referrals
                .Include(r => r.Referrer)
                .Include(r => r.ReferredUser)
                .SingleOrDefaultAsync(r => r.ReferralId == referralId);

            if(referral == null)
            {
                return NotFound("Referral Not Found");
            }

            return Ok(new 
            {
                referrer = referral.Referrer.Username,
                referredUser = referral.ReferredUser.Username,
                status = referral.Status,
                reward = referral.Reward,
            });

        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
