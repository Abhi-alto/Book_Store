using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.CartModel
{
    public class UpdateCart
    {
        [Required]
        public int CartId { get; set; }
        public int CartQuantity { get; set; }
    }
}
