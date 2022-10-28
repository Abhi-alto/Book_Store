using CommonLayer.BookModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
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
                cmd.Parameters.AddWithValue("@bookImg", bookModel.BookImg);
                cmd.Parameters.AddWithValue("@authorName", bookModel.AuthorName);
                cmd.Parameters.AddWithValue("@rating", bookModel.Rating);
                cmd.Parameters.AddWithValue("@rating_Count", bookModel.Rating_Count);
                cmd.Parameters.AddWithValue("@actualPrice", bookModel.ActualPrice);
                cmd.Parameters.AddWithValue("@discountedPrice", bookModel.DiscountedPrice);
                cmd.Parameters.AddWithValue("@quantity", bookModel.Quantity);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateBook(int bookID, UpdateBookModel bookModel)
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
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
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
        public List<GetAllBooksModel> GetAllBooks()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllBooks", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader READ = cmd.ExecuteReader();
                List<GetAllBooksModel> books = new List<GetAllBooksModel>();
                if (READ.HasRows)
                {
                    while (READ.Read())
                    {
                        GetAllBooksModel getAllBooksModel = new GetAllBooksModel();
                        getAllBooksModel.Id = READ.GetInt32(0);
                        getAllBooksModel.BookName = READ.GetString(1);
                        getAllBooksModel.BookDescription = READ.GetString(2);
                        getAllBooksModel.BookImg = READ.GetString(3);
                        getAllBooksModel.AuthorName = READ.GetString(4);
                        getAllBooksModel.Rating = READ.GetDecimal(5);
                        getAllBooksModel.Rating_Count = READ.GetInt32(6);
                        getAllBooksModel.ActualPrice = READ.GetInt32(7);
                        getAllBooksModel.DiscountedPrice = READ.GetInt32(8);
                        getAllBooksModel.Quantity = READ.GetInt32(9);
                        books.Add(getAllBooksModel);
                    }
                    return books;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GetAllBooksModel GetBook(int bookId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetBookById", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@bookId", bookId);
                SqlDataReader READ = cmd.ExecuteReader();
                GetAllBooksModel getAllBooksModel = new GetAllBooksModel();
                /*if(cmd.ExecuteNonQuery()==0)
                {
                    return null;
                }*/
                if (READ.HasRows)
                {
                    READ.Read();
                    getAllBooksModel.Id = READ.GetInt32(0);
                    getAllBooksModel.BookName = READ.GetString(1);
                    getAllBooksModel.BookDescription = READ.GetString(2);
                    getAllBooksModel.BookImg = READ.GetString(3);
                    getAllBooksModel.AuthorName = READ.GetString(4);
                    getAllBooksModel.Rating = READ.GetDecimal(5);
                    getAllBooksModel.Rating_Count = READ.GetInt32(6);
                    getAllBooksModel.ActualPrice = READ.GetInt32(7);
                    getAllBooksModel.DiscountedPrice = READ.GetInt32(8);
                    getAllBooksModel.Quantity = READ.GetInt32(9);
                }
                return getAllBooksModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}