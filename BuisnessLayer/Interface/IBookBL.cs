using CommonLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface IBookBL
    {
        public void AddBook(BookModel bookModel);
        public int UpdateBook(int bookID, UpdateBookModel bookModel);
        public bool RemoveBook(int bookID);
        public List<GetAllBooksModel> GetAllBooks();
        public GetAllBooksModel GetBook(int bookId);
    }
}
