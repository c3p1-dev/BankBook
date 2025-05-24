using System.ComponentModel.DataAnnotations;
using System;

namespace BankBook.Data.Models
{
    internal class UserAccount
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }
}
