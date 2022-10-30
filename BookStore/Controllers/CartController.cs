using BuisnessLayer.Interface;
using CommonLayer.BookModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using CommonLayer.CartModel;
using BookStore;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CartController : ControllerBase
    {
        ICartBL cartBL;
        private IConfiguration _config;
        public CartController(ICartBL cartBL, IConfiguration config)
        {
            this.cartBL = cartBL;
            this._config = config;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddCart")]
        public IActionResult AddCart(CartModel cartModel)
        {
            try
            {
                this.cartBL.AddCart(cartModel);
                return this.Ok(new { success = true, status = 200, message = "Book Added to cart successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpPut("UpdateCart/{cartID}/{bookId}")]
        public IActionResult UpdateCart(int cartID,int bookId, UpdateCart updateCart)
        {
            try
            {
                int result = this.cartBL.UpdateCart(cartID,bookId, updateCart);
                if (result == 0)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "Provide correct cartId" });
                }
                return this.Ok(new { success = true, status = 200, message = "Cart Updated successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteCart/{cartID}")]
        public IActionResult DeleteCart(int cartID)
        {
            try
            {
                bool result = this.cartBL.DeleteCart(cartID);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "Provide correct bookId" });
                }
                return this.Ok(new { success = true, status = 200, message = "Book deleted from cart successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpGet("GetCart")]
        public IActionResult GetCart()
        {
            try
            {
                // List<GetAllBooksModel> booksList = new List<GetAllBooksModel>();
                var booksList = this.cartBL.GetCart();
                if (booksList == null)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "Cart Empty" });
                }
                return this.Ok(new { success = true, status = 200, BooksList = booksList, message = "Books found successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
