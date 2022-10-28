using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.BookModel
{
    public class UpdateBookModel
    {
        [Required]
        public string BookDescription { get; set; }
        public decimal Rating { get; set; }

        [Required]
        public int Rating_Count { get; set; }

        [Required]
        public int ActualPrice { get; set; }
        [Required]
        public int DiscountedPrice { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
