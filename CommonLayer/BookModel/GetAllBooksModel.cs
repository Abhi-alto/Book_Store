using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.BookModel
{
    public class GetAllBooksModel
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string BookDescription { get; set; }
        public string BookImg { get; set; }
        public string AuthorName { get; set; }
        public decimal Rating { get; set; }
        public int Rating_Count { get; set; }
        public int ActualPrice { get; set; }
        public int DiscountedPrice { get; set; }
        public int Quantity { get; set; }
    }
}
