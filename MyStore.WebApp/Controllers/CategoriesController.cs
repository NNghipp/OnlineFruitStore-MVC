using Microsoft.AspNetCore.Mvc;
using MyStore.Business.Entities;
using MyStore.Services;

namespace MyStore.WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                // Check duplicate name
                var existingCategories = _categoryService.GetAllCategories();
                if (existingCategories.Any(c => c.CategoryName.Equals(category.CategoryName, StringComparison.OrdinalIgnoreCase)))
                {
                    TempData["ErrorMessage"] = $"Danh mục \"{category.CategoryName}\" đã tồn tại! Vui lòng chọn tên khác.";
                    return View(category);
                }

                _categoryService.CreateCategory(category);
                TempData["SuccessMessage"] = "Tạo danh mục thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check duplicate name (trừ category hiện tại)
                var existingCategories = _categoryService.GetAllCategories();
                if (existingCategories.Any(c => 
                    c.CategoryName.Equals(category.CategoryName, StringComparison.OrdinalIgnoreCase) && 
                    c.CategoryID != category.CategoryID))
                {
                    TempData["ErrorMessage"] = $"Danh mục \"{category.CategoryName}\" đã tồn tại! Vui lòng chọn tên khác.";
                    return View(category);
                }

                _categoryService.UpdateCategory(category);
                TempData["SuccessMessage"] = "Cập nhật danh mục thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryService.DeleteCategory(id);
            TempData["SuccessMessage"] = "Xóa danh mục thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
