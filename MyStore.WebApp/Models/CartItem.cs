namespace MyStore.WebApp.Models
{
    /// <summary>
    /// Represents an item in the shopping cart.
    /// </summary>
    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageFile { get; set; } = "default.png";
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
