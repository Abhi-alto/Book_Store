using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.WishList
{
    public class GetWishListModel
    {
        [Required]
        public int WishListId { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
    }
}
