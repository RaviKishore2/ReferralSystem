using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Referral_System.Models
{
    public class Referral
    {
        [Key]
        public int ReferralId { get; set; }

        [Required]
        public int ReferrerId { get; set; }

        [Required]
        public int ReferredUserId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; }

        public decimal Reward { get; set; }

        [ForeignKey("ReferrerId")]
        public User Referrer { get; set; }

        [ForeignKey("ReferredUserId")]
        public User ReferredUser { get; set; }
    }
}
