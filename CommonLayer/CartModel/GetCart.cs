using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.CartModel
{
    public class GetCart
    {
        public int CartId { get; set; }

        public int BookId { get; set; }

        public int Id { get; set; }

        public int CartQuantity { get; set; }
    }
}
