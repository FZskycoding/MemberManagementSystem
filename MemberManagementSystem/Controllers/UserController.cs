using Microsoft.AspNetCore.Mvc;
using MemberManagementSystem.Reopsitories;
using MemberManagementSystem.ViewModels;
using MemberManagementSystem.Models;

namespace MemberManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var users = _userRepository.GetAll();  // 呼叫 Repository 拿使用者清單
            return View(users);  // 將 users 傳到 View 顯示
        }



        // 顯示註冊表單
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // 接收表單資料
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);// 驗證失敗，回到註冊畫面
            }

            //建立新的User 
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
            };
            _userRepository.Add(user);
            _userRepository.Save();
            TempData["Register_Message"] = "註冊成功!";

            return RedirectToAction("Index");

        }

        //進入Login畫面
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);// 驗證失敗，回到註冊畫面
            }

            var user = _userRepository.GetByEmail(model.Email);
            if (user == null || user.Password != model.Password)
            {
                ModelState.AddModelError(string.Empty, "Email或密碼錯誤");
                return View(model);
            }
            HttpContext.Session.SetString("LoginName", user.Name);
            HttpContext.Session.SetString("LoginEmail", user.Email);

            // TempData["Login_Message"] = $"歡迎{user.Name}登入!";
            return RedirectToAction("Index");
        }

        //登出功能
        public IActionResult Logout()
        {
            // 清除登入狀態（這裡使用 TempData 模擬登入狀態，實際專案會用 Session 或 Identity）
            // TempData.Remove("Message");
            HttpContext.Session.Remove("LoginName");
            HttpContext.Session.Remove("LoginEmail");

            return RedirectToAction("Login");
        }
        
        //編輯使用者
        public IActionResult Edit(int id)
        {
            var user = _userRepository.GetById(id);
            
            if (user == null)
            {
                return NotFound();
            }

            // 確認登入使用者是否為該帳號本人
            string? LoginEmail = HttpContext.Session.GetString("LoginEmail");

            if (LoginEmail == null || user.Email != LoginEmail)
            {
                return RedirectToAction("Index", "Home"); // 或者 RedirectToAction("Index")
            }
            Console.WriteLine(user.Email);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            Console.WriteLine(user.Email);
            string? LoginEmail = HttpContext.Session.GetString("LoginEmail");
            if (LoginEmail == null || user.Email != LoginEmail)
            {
                Console.WriteLine("Edit 2 wrong");
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _userRepository.Update(user);
            _userRepository.Save();
            HttpContext.Session.SetString("LoginName", user.Name);

            return RedirectToAction("Index");
        }


        //刪除使用者
        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            string? LoginEmail = HttpContext.Session.GetString("LoginEmail");
            if (LoginEmail == null || user.Email != LoginEmail)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            string? LoginEmail = HttpContext.Session.GetString("LoginEmail");
            if (LoginEmail == null || user.Email != LoginEmail)
            {
                return RedirectToAction("Index", "Home");
            }

            _userRepository.Delete(id);
            _userRepository.Save();

            // 避免刪除後還保留登入狀態，可考慮清除 Session
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }

    }
}
