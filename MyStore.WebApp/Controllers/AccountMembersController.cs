using Microsoft.AspNetCore.Mvc;
using MyStore.Business.Entities;
using MyStore.Services;

namespace MyStore.WebApp.Controllers
{
    public class AccountMembersController : Controller
    {
        private readonly IAccountMemberService _accountMemberService;

        public AccountMembersController(IAccountMemberService accountMemberService)
        {
            _accountMemberService = accountMemberService;
        }

        public IActionResult Index()
        {
            var members = _accountMemberService.GetAllMembers();
            return View(members);
        }

        public IActionResult Details(int id)
        {
            var member = _accountMemberService.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AccountMember member)
        {
            if (ModelState.IsValid)
            {
                _accountMemberService.CreateMember(member);
                TempData["SuccessMessage"] = "Member created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public IActionResult Edit(int id)
        {
            var member = _accountMemberService.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AccountMember member)
        {
            if (id != member.MemberID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _accountMemberService.UpdateMember(member);
                TempData["SuccessMessage"] = "Member updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public IActionResult Delete(int id)
        {
            var member = _accountMemberService.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _accountMemberService.DeleteMember(id);
            TempData["SuccessMessage"] = "Member deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
