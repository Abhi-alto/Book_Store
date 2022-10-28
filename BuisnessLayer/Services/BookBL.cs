using BuisnessLayer.Interface;
using CommonLayer.BookModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class BookBL:IBookBL
    {
        IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public void AddBook(BookModel bookModel)
        {
            try
            {
                this.bookRL.AddBook(bookModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public bool RemoveBook(int bookID)
        {
            try
            {
                return this.bookRL.RemoveBook(bookID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateBook(int bookID,UpdateBookModel bookModel)
        {
            try
            {
                return this.bookRL.UpdateBook(bookID,bookModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GetAllBooksModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GetAllBooksModel GetBook(int BookId)
        {
            try
            {
                return this.bookRL.GetBook(BookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
