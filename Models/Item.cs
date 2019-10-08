using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BeltExam.Models
{
    public class Item
    {
        [Key]
        public int ItemId {get;set;}
        [Required]
        [Display(Name = "Item Name")]
        public string Name {get;set;}
        [Required]
        [CheckFuture]
        [DataType(DataType.Date)]
        public DateTime End {get;set;}
        [Required]
        [Range(0, Int32.MaxValue, ErrorMessage="cannot be negative")]
        [Display(Name = "Starting Bid")]
        public int StartingBid {get;set;}
        [Required]
        [MinLength(10)]
        [DataType(DataType.Text)]
        public string Description {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        // Not Mapped
        [NotMapped]
        public int TimeRemaining
        {
            get
            {
                return (int)(this.End - DateTime.Now).TotalDays;
            }
        }
        [NotMapped]
        public string TimeRemainingText
        {
            get
            {
            TimeSpan span = (this.End - DateTime.Now);
            if(span.Days == 0)
            {
                return $"{span.Hours} hours, {span.Minutes} minutes, {span.Seconds} seconds";
            }
            if(span.Hours == 0)
            {
                return $"{span.Minutes} minutes, {span.Seconds} seconds";
            }
            if(span.Minutes == 0)
            {
                return $"{span.Seconds} seconds";
            }
            return $"{span.Days} days, {span.Hours} hours, {span.Minutes} minutes, {span.Seconds} seconds";
            }
        }
        // Link & Navigation
        [Required]
        public int UserId {get;set;}
        public User Seller {get;set;}
        public ICollection<Bid> Bids {get;set;}
    }
    public class CheckFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime start = (DateTime)value;
            DateTime today = DateTime.Today;
            double SignedDays = (start - today).TotalDays;
            if(SignedDays < 0)
            {
                return new ValidationResult($"Auction must end in the future");
            }
            return ValidationResult.Success;
        }
    }
}