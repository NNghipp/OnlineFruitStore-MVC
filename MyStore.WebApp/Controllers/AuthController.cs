using Microsoft.AspNetCore.Mvc;
using MyStore.Services;
using MyStore.WebApp.Models;
using MyStore.Business.Entities;

namespace MyStore.WebApp.Controllers
{
    /// <summary>
    /// Controller xử lý đăng nhập, đăng ký, đăng xuất.
    /// Sử dụng Session để lưu thông tin user đã đăng nhập.
    /// </summary>
    public class AuthController : Controller
    {
        private readonly IAccountMemberService _memberService;

        public AuthController(IAccountMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: /Auth/Login
        public IActionResult Login(string? returnUrl = null)
        {
            // Nếu đã đăng nhập, redirect về Home
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Tìm user theo email và password
            var members = _memberService.GetAllMembers();
            var user = members.FirstOrDefault(m => 
                m.EmailAddress == model.Email && 
                m.MemberPassword == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng!");
                return View(model);
            }

            // Lưu thông tin user vào Session
            HttpContext.Session.SetInt32("UserId", user.MemberID);
            HttpContext.Session.SetString("UserName", user.FullName ?? "User");
            HttpContext.Session.SetString("UserEmail", user.EmailAddress ?? "");
            HttpContext.Session.SetString("UserRole", user.MemberRole ?? "User");

            TempData["SuccessMessage"] = $"Chào mừng {user.FullName}!";

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            
            return RedirectToAction("Index", "Home");
        }

        // GET: /Auth/Register
        public IActionResult Register()
        {
            // Nếu đã đăng nhập, redirect về Home
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check email đã tồn tại chưa
            var members = _memberService.GetAllMembers();
            if (members.Any(m => m.EmailAddress == model.Email))
            {
                ModelState.AddModelError("Email", "Email này đã được sử dụng!");
                return View(model);
            }

            // Tạo tài khoản mới
            var newMember = new AccountMember
            {
                FullName = model.FullName,
                EmailAddress = model.Email,
                MemberPassword = model.Password, // Trong thực tế nên hash password
                PhoneNumber = model.PhoneNumber,
                MemberRole = "User" // Mặc định là User
            };

            _memberService.CreateMember(newMember);

            TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            return RedirectToAction("Login");
        }

        // POST: /Auth/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Xóa Session
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Đã đăng xuất thành công!";
            return RedirectToAction("Index", "Home");
        }
    }
}
