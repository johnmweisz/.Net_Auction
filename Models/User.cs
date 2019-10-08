using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BeltExam.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        [Required]
        [Display(Name = "First Name")]
        public string FirstName {get;set;}
        [Required]
        [Display(Name = "Last Name")]
        public string LastName {get;set;}
        [Required]
        [EmailAddress]
        public string Email {get;set;}
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage="Minimum eight characters, at least one letter, one number and one special character.")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        public int Wallet {get;set;} = 1000;
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        // Not Mapped
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string Confirm {get;set;}
        // Link & Navigation
        public ICollection<Item> ItemsForSale {get;set;}
        public ICollection<Bid> Bids {get;set;}
    }
}