using Microsoft.AspNetCore.Mvc;
using MyStore.Services;
using MyStore.WebApp.Models;

namespace MyStore.WebApp.Controllers
{
    /// <summary>
    /// Controller quản lý thông tin cá nhân user.
    /// Yêu cầu đăng nhập để truy cập.
    /// </summary>
    public class ProfileController : Controller
    {
        private readonly IAccountMemberService _memberService;

        public ProfileController(IAccountMemberService memberService)
        {
            _memberService = memberService;
        }

        // Helper: Kiểm tra đã đăng nhập chưa
        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetInt32("UserId") != null;
        }

        // Helper: Lấy UserId từ Session
        private int GetUserId()
        {
            return HttpContext.Session.GetInt32("UserId") ?? 0;
        }

        // GET: /Profile
        public IActionResult Index()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth", new { returnUrl = "/Profile" });
            }

            var member = _memberService.GetMemberById(GetUserId());
            if (member == null)
            {
                return RedirectToAction("Logout", "Auth");
            }

            return View(member);
        }

        // GET: /Profile/ChangePassword
        public IActionResult ChangePassword()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        // POST: /Profile/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var member = _memberService.GetMemberById(GetUserId());
            if (member == null)
            {
                return RedirectToAction("Logout", "Auth");
            }

            // Kiểm tra mật khẩu hiện tại
            if (member.MemberPassword != model.CurrentPassword)
            {
                ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng!");
                return View(model);
            }

            // Cập nhật mật khẩu mới
            member.MemberPassword = model.NewPassword;
            _memberService.UpdateMember(member);

            TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("Index");
        }

        // GET: /Profile/UpdateContact
        public IActionResult UpdateContact()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            var member = _memberService.GetMemberById(GetUserId());
            if (member == null)
            {
                return RedirectToAction("Logout", "Auth");
            }

            var model = new UpdateContactViewModel
            {
                FullName = member.FullName ?? "",
                Email = member.EmailAddress ?? "",
                PhoneNumber = member.PhoneNumber,
                Address = member.Address
            };

            return View(model);
        }

        // POST: /Profile/UpdateContact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateContact(UpdateContactViewModel model)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var member = _memberService.GetMemberById(GetUserId());
            if (member == null)
            {
                return RedirectToAction("Logout", "Auth");
            }

            // Kiểm tra email đã tồn tại (trừ email hiện tại)
            var allMembers = _memberService.GetAllMembers();
            if (allMembers.Any(m => m.EmailAddress == model.Email && m.MemberID != member.MemberID))
            {
                ModelState.AddModelError("Email", "Email này đã được sử dụng bởi tài khoản khác!");
                return View(model);
            }

            // Cập nhật thông tin
            member.FullName = model.FullName;
            member.EmailAddress = model.Email;
            member.PhoneNumber = model.PhoneNumber;
            member.Address = model.Address;

            _memberService.UpdateMember(member);

            // Cập nhật Session
            HttpContext.Session.SetString("UserName", member.FullName ?? "User");
            HttpContext.Session.SetString("UserEmail", member.EmailAddress ?? "");

            TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
            return RedirectToAction("Index");
        }
    }
}
