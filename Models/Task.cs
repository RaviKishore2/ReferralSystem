using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Referral_System.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public int UserId { get; set; }

        public bool CompletionStatus { get; set; }

        [Required]
        public string Details { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        //DBCC CHECKIDENT ('Users',RESEED,0)
    }
}
