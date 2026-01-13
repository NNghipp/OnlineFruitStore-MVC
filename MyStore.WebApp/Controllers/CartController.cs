using Microsoft.AspNetCore.Mvc;
using MyStore.Services;
using MyStore.WebApp.Services;
using MyStore.WebApp.Helpers;

namespace MyStore.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            ViewBag.CartTotal = _cartService.GetCartTotal();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = _productService.GetProductById(productId);
            if (product != null)
            {
                _cartService.AddToCart(product, quantity);
                TempData["SuccessMessage"] = $"Đã thêm \"{product.ProductName}\" vào giỏ hàng!";
            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AddToCartFromDetails(int productId, int quantity = 1)
        {
            var product = _productService.GetProductById(productId);
            if (product != null)
            {
                _cartService.AddToCart(product, quantity);
                TempData["SuccessMessage"] = $"Đã thêm \"{product.ProductName}\" vào giỏ hàng!";
            }
            
            return RedirectToAction("Details", "Products", new { id = productId });
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            _cartService.UpdateQuantity(productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            TempData["SuccessMessage"] = "Đã xóa sản phẩm khỏi giỏ hàng!";
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCart();
            if (!cart.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng trống!";
                return RedirectToAction("Index");
            }
            
            ViewBag.CartTotal = _cartService.GetCartTotal();
            return View(cart);
        }

        [HttpPost]
        public IActionResult PlaceOrder(string fullName, string phone, string address)
        {
            // Demo only - In real app, save to database
            _cartService.ClearCart();
            TempData["SuccessMessage"] = $"Đặt hàng thành công! Cảm ơn {fullName} đã mua hàng. Chúng tôi sẽ liên hệ qua số {phone}.";
            return RedirectToAction("Index", "Home");
        }

        // API endpoint for getting cart count (for AJAX)
        [HttpGet]
        public IActionResult GetCartCount()
        {
            return Json(new { count = _cartService.GetCartCount() });
        }
    }
}
