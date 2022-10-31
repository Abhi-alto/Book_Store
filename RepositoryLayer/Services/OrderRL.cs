using CommonLayer.OrderModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRL : IOrderRL
    {
        IConfiguration _configuration;
        const string connectionString = @"Server=.;Database=Book_Store;Trusted_Connection=True;";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public OrderRL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AddOrderModel AddOrder(AddOrderModel addOrderModel)
        {
            try
            {
                SqlCommand price = new SqlCommand("spTotalPriceCart", sqlConnection);
                SqlCommand command = new SqlCommand("spAddOrder", sqlConnection);

                price.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                sqlConnection.Open();
                price.Parameters.AddWithValue("@cartId", addOrderModel.cartId);
                var Price = (decimal)price.ExecuteScalar();
                command.Parameters.AddWithValue("@orderDate", DateTime.Now);
                command.Parameters.AddWithValue("@totalPrice", Price);
                command.Parameters.AddWithValue("@AddressId", addOrderModel.AddressId);
                command.Parameters.AddWithValue("@cartId", addOrderModel.cartId);
                command.Parameters.AddWithValue("@bookId", addOrderModel.bookId);
                command.Parameters.AddWithValue("@userId", addOrderModel.userId);
                var response = command.ExecuteNonQuery();
                if (response > 0)
                {
                    return addOrderModel;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool DeleteOrder(int orderid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("delete from dbo.[Order] where OrderId = @orderId", sqlConnection);
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@OrderId", orderid);
                var row = cmd.ExecuteNonQuery();
                if (row > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetAllOrderModel> GetAllOrder(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
