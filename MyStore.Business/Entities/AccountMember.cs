using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStore.Business.Entities
{
    [Table("AccountMember")]
    public class AccountMember
    {
        [Key]
        public int MemberID { get; set; }

        [Required]
        [StringLength(255)]
        public string MemberPassword { get; set; } = null!;

        [StringLength(100)]
        public string? FullName { get; set; }

        [StringLength(100)]
        public string? EmailAddress { get; set; }

        [StringLength(50)]
        public string? MemberRole { get; set; }
    }
}
