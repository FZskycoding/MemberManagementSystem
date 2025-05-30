using System.ComponentModel.DataAnnotations;

namespace MemberManagementSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="請輸入姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入 Email")]
        [EmailAddress(ErrorMessage = "Email 格式錯誤")]
        public string Email { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="密碼至少6碼")]
        public string Password { get; set; }

        [Required(ErrorMessage = "請再次輸入密碼")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="兩次密碼不一致")]
        public string ComfirmPassword { get; set; }



    }
}
