using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Experimental.System.Messaging;
using System.Text;
using Microsoft.Extensions.Configuration;
using CommonLayer.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RepositoryLayer.Interface;
using CommonLayer.CartModel;
using System.Reflection;
using System.Net;
using CommonLayer.BookModel;

namespace RepositoryLayer.Services
{
    public class CartRL : ICartRL
    {
        private IConfiguration _config;
        const string connectionString = @"Server=.;Database=Book_Store;Trusted_Connection=True;";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public CartRL(IConfiguration config)
        {
            this._config = config;
        }
        public void AddCart(CartModel cartModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("AddCart", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@bookId", cartModel.BookId);
                cmd.Parameters.AddWithValue("@id",cartModel.Id);
                cmd.Parameters.AddWithValue("@cartQuantity", cartModel.CartQuantity);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateCart(int CartId,int BookId, UpdateCart updateCart)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UpdateCart", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@cartId", CartId);
                cmd.Parameters.AddWithValue("@bookId", BookId);
                cmd.Parameters.AddWithValue("@cartQuantity",updateCart.CartQuantity);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteCart(int CartId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteCart", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@cartId", CartId);
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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<GetCart> GetCart()
        {

            try
            {
                SqlCommand cmd = new SqlCommand("GetCart", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader READ = cmd.ExecuteReader();
                List<GetCart> books= new List<GetCart>();
                if (READ.HasRows)
                {
                    while (READ.Read())
                    {
                        GetCart cartModel = new GetCart();
                        cartModel.CartId = READ.GetInt32(0);
                        cartModel.BookId = READ.GetInt32(1);
                        cartModel.Id = READ.GetInt32(2);
                        cartModel.CartQuantity = READ.GetInt32(3);
                        books.Add(cartModel);
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
