using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.BookModel
{
    public class BookModel
    {
        [Required]
        //[RegularExpression(@"^[A-Z]{1}[A-Za-z]{3,}", ErrorMessage = "Start with a capital letter and must have minimum three letters")]
        public string BookName { get; set; }
        [Required]
        public string BookDescription { get; set; }
        [Required]
        public string BookImg { get; set; }
        [Required]
        public string AuthorName { get; set; }

        [Required]
        public decimal Rating { get; set; }

        [Required]
        public int Rating_Count { get; set; }

        [Required]
        public int ActualPrice{ get; set; }
        [Required]
        public int DiscountedPrice { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
