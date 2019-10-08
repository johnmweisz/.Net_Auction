using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BeltExam.Models
{
    public class Display
    {
        public Item Item {get;set;}
        public Bid Bid {get;set;}
    }
}