using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore.Business.Entities;
using MyStore.Services;
using MyStore.WebApp.Helpers;

namespace MyStore.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(
            IProductService productService, 
            ICategoryService categoryService,
            IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products (với filter theo Category)
        public IActionResult Index(int? categoryId)
        {
            var products = _productService.GetAllProducts();
            
            // Filter theo category nếu có
            if (categoryId.HasValue && categoryId > 0)
            {
                products = products.Where(p => p.CategoryID == categoryId);
            }

            // Truyền danh sách categories cho dropdown filter
            ViewBag.Categories = new SelectList(
                _categoryService.GetAllCategories(), 
                "CategoryID", 
                "CategoryName",
                categoryId);
            ViewBag.SelectedCategoryId = categoryId;

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                // Check duplicate name
                var existingProducts = _productService.GetAllProducts();
                if (existingProducts.Any(p => p.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
                {
                    TempData["ErrorMessage"] = $"Sản phẩm \"{product.ProductName}\" đã tồn tại! Vui lòng chọn tên khác.";
                    ViewBag.Categories = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName", product.CategoryID);
                    return View(product);
                }

                // Xử lý upload ảnh
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "products");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    product.ImageFile = uniqueFileName;
                }
                else
                {
                    // Sử dụng FruitImageHelper để tự động chọn ảnh theo tên
                    product.ImageFile = FruitImageHelper.GetImageByProductName(product.ProductName);
                }

                _productService.CreateProduct(product);
                TempData["SuccessMessage"] = "Tạo sản phẩm thành công!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile? imageFile)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check duplicate name (trừ sản phẩm hiện tại)
                var existingProducts = _productService.GetAllProducts();
                if (existingProducts.Any(p => 
                    p.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase) && 
                    p.ProductID != product.ProductID))
                {
                    TempData["ErrorMessage"] = $"Sản phẩm \"{product.ProductName}\" đã tồn tại! Vui lòng chọn tên khác.";
                    ViewBag.Categories = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName", product.CategoryID);
                    return View(product);
                }

                // Xử lý upload ảnh mới
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "products");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    product.ImageFile = uniqueFileName;
                }

                _productService.UpdateProduct(product);
                TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productService.DeleteProduct(id);
            TempData["SuccessMessage"] = "Xóa sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}
