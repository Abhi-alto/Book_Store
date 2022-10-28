using BuisnessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using CommonLayer.BookModel;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        IBookBL bookBL;
        private IConfiguration _config;
        public BookController(IBookBL bookBL, IConfiguration config,int bookID)
        {
            this.bookBL = bookBL;
            this._config = config;
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddBook")]
        public IActionResult AddNote(BookModel bookModel)
        {
            try
            {
                this.bookBL.AddBook(bookModel);
                return this.Ok(new { success = true, status = 200, message = "Book Added successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateNote/{bookID}")]
        public IActionResult UpdateNote(int bookID,UpdateBookModel bookModel)
        {
            try
            {
                int result=this.bookBL.UpdateBook(bookID,bookModel);
                if (result==0)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "Provide correct bookId" });
                }
                return this.Ok(new { success = true, status = 200, message = "Book Updated successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPut("DeleteBook/{bookID}")]
        public IActionResult RemoveBook(int bookID)
        {
            try
            {
                bool result = this.bookBL.RemoveBook(bookID);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "Provide correct bookId" });
                }
                return this.Ok(new { success = true, status = 200, message = "Book deleted successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
