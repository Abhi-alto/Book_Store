using BuisnessLayer.Interface;
using CommonLayer.CartModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using CommonLayer.WishList;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WishListController : Controller
    {
        IWishListBL wishListBL;
        private IConfiguration _config;
        public WishListController(IWishListBL wishListBL, IConfiguration config)
        {
            this.wishListBL = wishListBL;
            this._config = config;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AAddToWishList")]
        public IActionResult AddToWishList(WishListModel wishListModel)
        {
            try
            {
                this.wishListBL.AddToWishList(wishListModel);
                return this.Ok(new { success = true, status = 200, message = "Book Added to cart successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteWishList/{WishListId}")]
        public IActionResult DeleteWishList(int WishListId)
        {
            try
            {
                bool result = this.wishListBL.DeleteWishList(WishListId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "Provide correct WishListId" });
                }
                return this.Ok(new { success = true, status = 200, message = "Book deleted from Wish List" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpGet("GetWishList")]
        public IActionResult GetWishList()
        {
            try
            {
                var booksList = this.wishListBL.GetWishList();
                if (booksList == null)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "Wish List Empty" });
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
