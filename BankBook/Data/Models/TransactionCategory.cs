using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankBook.Data.Models
{
    public class TransactionCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Key { get; set; }

        [Required(ErrorMessage = "Category Code is required")]
        [StringLength(10, ErrorMessage = "Category Code is at most 10 characters")]
        [RegularExpression("^[A-Z]{1,10}$")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(10, ErrorMessage = "Category Name is at most 100 characters")]
        public string Name { get; set; } = string.Empty;
    }
}
