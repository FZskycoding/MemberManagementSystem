using System.ComponentModel.DataAnnotations;

namespace MemberManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        // 你可以視情況加入其他欄位，例如 Role、CreatedDate 等
    }
}
