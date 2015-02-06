using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWebsite.Domain;

namespace MyWebsite.Models
{
    public class TopMenuViewModel
    {
        public List<CartItemResult> Carts { get; set; }
        public List<CompareItemResult> CompareItems { get; set; } 
    }
}