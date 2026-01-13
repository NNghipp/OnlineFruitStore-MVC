namespace MyStore.WebApp.Helpers
{
    /// <summary>
    /// Helper class to map product names to corresponding fruit images.
    /// Uses simple keyword matching: "táo" = apple.png, "chuối" = banana.png, etc.
    /// </summary>
    public static class FruitImageHelper
    {
        private static readonly Dictionary<string, string> FruitMapping = new()
        {
            { "táo", "apple.png" },
            { "tao", "apple.png" },
            { "apple", "apple.png" },
            { "chuối", "banana.png" },
            { "chuoi", "banana.png" },
            { "banana", "banana.png" },
            { "cam", "orange.png" },
            { "orange", "orange.png" },
            { "đào", "peach.jpg" },
            { "dao", "peach.jpg" },
            { "peach", "peach.jpg" },
            { "dâu", "strawberry.png" },
            { "dau", "strawberry.png" },
            { "strawberry", "strawberry.png" }
        };

        /// <summary>
        /// Gets the image filename based on the product name.
        /// Searches for matching keywords in the product name.
        /// </summary>
        /// <param name="productName">The product name to match</param>
        /// <returns>The corresponding image filename, or "default.png" if no match found</returns>
        public static string GetImageByProductName(string productName)
        {
            if (string.IsNullOrEmpty(productName))
                return "default.png";

            string lowerName = productName.ToLower();

            foreach (var mapping in FruitMapping)
            {
                if (lowerName.Contains(mapping.Key))
                {
                    return mapping.Value;
                }
            }

            return "default.png";
        }
    }
}
