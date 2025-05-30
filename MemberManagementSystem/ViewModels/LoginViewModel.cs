using System.ComponentModel.DataAnnotations;

namespace MemberManagementSystem.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="請輸入Email")]
        [EmailAddress(ErrorMessage ="Email格式不正確")]
        public string Email { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
