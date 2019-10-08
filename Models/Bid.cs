using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BeltExam.Models
{
    public class Bid
    {
        [Key]
        public int BidId {get;set;}
        public int ItemId {get;set;}
        public int UserId {get;set;}
        [Required]
        [Display(Name = "Bid")]
        public int Ammount {get;set;}
        public User User {get;set;}
        public Item Item {get;set;}
    }
    
}