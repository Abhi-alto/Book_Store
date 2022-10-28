using BuisnessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using CommonLayer.BookModel;
using System.Collections.Generic;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BookController : ControllerBase
    {
        IBookBL bookBL;
        private IConfiguration _config;
        public BookController(IBookBL bookBL, IConfiguration config)
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
        [HttpDelete("DeleteBook/{bookID}")]
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
        [Authorize]
        [HttpGet("GetAllBook")]
        public IActionResult GetAllBooks()
        {
            try
            {
               // List<GetAllBooksModel> booksList = new List<GetAllBooksModel>();
               var booksList=this.bookBL.GetAllBooks();
                if(booksList==null)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "No books found" });
                }
                return this.Ok(new { success = true, status = 200,BooksList=booksList, message = "Books found successfully" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetBook")]
        public IActionResult GetBook(int bookId)
        {
            try
            {
                
                var book = this.bookBL.GetBook(bookId);
                if (book == null)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "No books found" });
                }
                return this.Ok(new { success = true, status = 200, Book = book, message = "Book found" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
