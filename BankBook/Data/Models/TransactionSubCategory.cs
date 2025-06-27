using System.ComponentModel.DataAnnotations;

namespace BankBook.Data.Models
{
    public class TransactionSubCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Sub-Category Code is required")]
        [StringLength(10, ErrorMessage = "Sub-Category Code is at most 10 characters")]
        [RegularExpression("^[A-Z]{1,10}$")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sub-Category Name is required")]
        [StringLength(10, ErrorMessage = "Sub-Category Name is at most 100 characters")]
        [RegularExpression("^[A-Z]{1,100}$")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category Code is required")]
        [StringLength(10, ErrorMessage = "Category Code is at most 10 characters")]
        public string Category { get; set; } = string.Empty;
    }
}
