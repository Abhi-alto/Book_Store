using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.CartModel
{
    public class CartModel
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public int CartQuantity { get; set; }
    }
}
