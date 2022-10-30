using CommonLayer.CartModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using CommonLayer.WishList;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Services
{
    public class WishListRL : IWishListRL
    {
        private IConfiguration _config;
        const string connectionString = @"Server=.;Database=Book_Store;Trusted_Connection=True;";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public WishListRL(IConfiguration config)
        {
            this._config = config;
        }
        public void AddToWishList(WishListModel wishListModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("AddToWishList", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@id", wishListModel.Id);
                cmd.Parameters.AddWithValue("@bookId",wishListModel.BookId);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public bool DeleteWishList(int WishListId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteFromWishList", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@wishListId", WishListId);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetWishListModel> GetWishList()
        {

            try
            {
                SqlCommand cmd = new SqlCommand("GetFromWishList", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader READ = cmd.ExecuteReader();
                List<GetWishListModel> books = new List<GetWishListModel>();
                if (READ.HasRows)
                {
                    while (READ.Read())
                    {
                        GetWishListModel Wish = new GetWishListModel();
                        Wish.WishListId = READ.GetInt32(0);
                        Wish.Id = READ.GetInt32(1);
                        Wish.BookId = READ.GetInt32(2);
                        books.Add(Wish);
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
    }
}
