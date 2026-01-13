using MyStore.Business.Entities;
using MyStore.WebApp.Models;
using MyStore.WebApp.Helpers;
using System.Text.Json;

namespace MyStore.WebApp.Services
{
    public interface ICartService
    {
        void AddToCart(Product product, int quantity = 1);
        void RemoveFromCart(int productId);
        void UpdateQuantity(int productId, int quantity);
        List<CartItem> GetCart();
        int GetCartCount();
        decimal GetCartTotal();
        void ClearCart();
    }

    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "ShoppingCart";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession? Session => _httpContextAccessor.HttpContext?.Session;

        public void AddToCart(Product product, int quantity = 1)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(x => x.ProductID == product.ProductID);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    ImageFile = FruitImageHelper.GetImageByProductName(product.ProductName),
                    UnitPrice = product.UnitPrice ?? 0,
                    Quantity = quantity
                });
            }

            SaveCart(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductID == productId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductID == productId);
            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
                SaveCart(cart);
            }
        }

        public List<CartItem> GetCart()
        {
            if (Session == null) return new List<CartItem>();

            var cartJson = Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }

            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        public int GetCartCount()
        {
            return GetCart().Sum(x => x.Quantity);
        }

        public decimal GetCartTotal()
        {
            return GetCart().Sum(x => x.TotalPrice);
        }

        public void ClearCart()
        {
            Session?.Remove(CartSessionKey);
        }

        private void SaveCart(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            Session?.SetString(CartSessionKey, cartJson);
        }
    }
}
