using Microsoft.AspNetCore.Mvc;
using Referral_System.DTO;
using Referral_System.Models;
using Task = Referral_System.Models.Task;

namespace Referral_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ReferralSystemDbContext _context;

        public TaskController(ReferralSystemDbContext context)
        {
            _context = context;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitTask([FromBody] TaskDto taskDto)
        {
            var user = await _context.Users.FindAsync(taskDto.UserId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var task = new Task
            {
                UserId = taskDto.UserId,
                Details = $"Bank Details: {taskDto.BankDetails}",
                CompletionStatus = true
            };

            _context.Tasks.Add(task);
            user.EligibilityStatus = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Task Completed successfully, you are eligible to refer others" });
        
        }

        [HttpGet("check-eligibility/{userId}")]
        public async Task<IActionResult> CheckEligibility(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if(user == null)
            {

                return NotFound("User not Found");
            }

            return Ok(new {eligibility = user.EligibilityStatus });
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
