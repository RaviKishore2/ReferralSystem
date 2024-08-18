using System.ComponentModel.DataAnnotations;

namespace Referral_System.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [MaxLength(50)]
        public string BankDetails { get; set; }

        [MaxLength(10)]
        public string PAN { get; set; }

        public DateTime DOB { get; set; }

        public bool EligibilityStatus { get; set; }

        public ICollection<Referral> ReferralsMade { get; set; }
        
        public ICollection<Referral> ReferralsReceived { get; set; }
    }
}
