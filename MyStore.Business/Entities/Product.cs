using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStore.Business.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = null!;

        public int? CategoryID { get; set; }

        public int? UnitsInStock { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? UnitPrice { get; set; }

        public string ImageFile { get; set; } = "default.png";

        [ForeignKey("CategoryID")]
        public virtual Category? Category { get; set; }
    }
}
