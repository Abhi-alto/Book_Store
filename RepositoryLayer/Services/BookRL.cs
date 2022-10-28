using CommonLayer.BookModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL:IBookRL
    {
        private IConfiguration _config;
        const string connectionString = @"Server=.;Database=Book_Store;Trusted_Connection=True;";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public BookRL(IConfiguration config)
        {
            this._config = config;
        }

        public void AddBook(BookModel bookModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("AddBook", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@bookName", bookModel.BookName);
                cmd.Parameters.AddWithValue("@bookDescription", bookModel.BookDescription);
                cmd.Parameters.AddWithValue("@bookImg",bookModel.BookImg);
                cmd.Parameters.AddWithValue("@authorName", bookModel.AuthorName);
                cmd.Parameters.AddWithValue("@rating", bookModel.Rating);
                cmd.Parameters.AddWithValue("@rating_Count", bookModel.Rating_Count);
                cmd.Parameters.AddWithValue("@actualPrice", bookModel.ActualPrice);
                cmd.Parameters.AddWithValue("@discountedPrice", bookModel.DiscountedPrice);
                cmd.Parameters.AddWithValue("@quantity", bookModel.Quantity);
                cmd.ExecuteNonQuery();
                    sqlConnection.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateBook(int bookID,UpdateBookModel bookModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UpdateBook", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@BookId", bookID);
                cmd.Parameters.AddWithValue("@bookDescription", bookModel.BookDescription);
                cmd.Parameters.AddWithValue("@rating", bookModel.Rating);
                cmd.Parameters.AddWithValue("@rating_Count", bookModel.Rating_Count);
                cmd.Parameters.AddWithValue("@actualPrice", bookModel.ActualPrice);
                cmd.Parameters.AddWithValue("@discountedPrice", bookModel.DiscountedPrice);
                cmd.Parameters.AddWithValue("@quantity", bookModel.Quantity);
                int rowsAffected=cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveBook(int bookID)
        {
            SqlCommand cmd = new SqlCommand("DeleteBook", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            sqlConnection.Open();
            cmd.Parameters.AddWithValue("@BookId", bookID);
            var result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
