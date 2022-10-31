using CommonLayer.AddressModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL : IAddressRL
    {
        IConfiguration _configuration;
        const string connectionString = @"Server=.;Database=Book_Store;Trusted_Connection=True;";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public AddressRL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AddAddressModel AddAddress(AddAddressModel addAddress)
        {
            try
            {
                SqlCommand command = new SqlCommand("AddAddress", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                command.Parameters.AddWithValue("@Address", addAddress.Address);
                command.Parameters.AddWithValue("@City", addAddress.City);
                command.Parameters.AddWithValue("@State", addAddress.State);
                command.Parameters.AddWithValue("@typeId", addAddress.typeId);
                command.Parameters.AddWithValue("@userId", addAddress.userId);
                var res = command.ExecuteNonQuery();
                sqlConnection.Close();
                if (res > 0)
                {
                    return addAddress;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UpdateAddressModel updateAddress(UpdateAddressModel updateAddress, int AddessId)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand command = new SqlCommand("UpdateAddress", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                command.Parameters.AddWithValue("@AddressId", AddessId);
                command.Parameters.AddWithValue("@Address", updateAddress.Address);
                command.Parameters.AddWithValue("@City", updateAddress.City);
                command.Parameters.AddWithValue("@State", updateAddress.State);
                command.Parameters.AddWithValue("@typeId", updateAddress.typeId);
                var res = command.ExecuteNonQuery();
                if (res > 0)
                {
                    return updateAddress;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool DeleteAddress(int AddressId)
        {
            SqlConnection conn = new SqlConnection(this._configuration.GetConnectionString("bookStore"));
            try
            {
                SqlCommand command = new SqlCommand("delete from dbo.AddressDetails where AddressId=@AddressId ", conn);
                sqlConnection.Open();
                command.Parameters.AddWithValue("@AddressId", AddressId);
                var rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

    }
}
