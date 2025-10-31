﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankBook.Data.Models
{
    public class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Account Code must be defined")]
        [StringLength(150, ErrorMessage = "Account Code too long (max 10 characters)")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account Name must be defined")]
        [StringLength(100, ErrorMessage = "Account name too long (max 100 characters)")]
        public string Name { get; set; } = string.Empty;

        [StringLength(150, ErrorMessage = "Bank name too long (max 150 characters)")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ][A-Za-zÀ-ÿ0-9 .'\-]{1,149}$", ErrorMessage = "Invalid bank name format")]
        public string Bank { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Description too long (max 255 characters)")]
        public string Description { get; set; } = string.Empty;

        [StringLength(11, ErrorMessage = "SWIFT/BIC too long (max 11 characters)")]
        [RegularExpression("^[A-Z]{4}[A-Z]{2}[A-Z0-9]{2}([A-Z0-9]{3})?$", ErrorMessage = "Invalid SWIFT/BIC format")]
        public string Swift { get; set; } = string.Empty;

        [StringLength(34, ErrorMessage = "IBAN too long (max 34 characters)")]
        [RegularExpression("^[A-Z]{2}[0-9]{2}[A-Z0-9]{11,30}$", ErrorMessage = "Invalid IBAN format")]
        public string IBAN { get; set; } = string.Empty;

        [StringLength(2083, ErrorMessage = "URL too long (max 2083 characters)")]
        public string Url { get; set; } = string.Empty;

        public decimal Balance { get; set; } = decimal.Zero;

        public DateOnly? LockedAt { get; set; }
    }
}