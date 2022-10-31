using CommonLayer.FeedbackModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FeedbackRL:IFeedbackRL
    {
        IConfiguration _configuration;
        const string connectionString = @"Server=.;Database=Book_Store;Trusted_Connection=True;";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public FeedbackRL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AddFeedbackModel Addfeedback(AddFeedbackModel addFeedbackModel)
        { 
            try
            {
                SqlCommand cmd = new SqlCommand("AddFeedback", sqlConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@rating", addFeedbackModel.Rating);
                cmd.Parameters.AddWithValue("@comment", addFeedbackModel.comment);
                cmd.Parameters.AddWithValue("@bookId", addFeedbackModel.bookId);
                cmd.Parameters.AddWithValue("@userId", addFeedbackModel.userId);
                var rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    return addFeedbackModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetFeedbackModel> GetFeedback(int bookId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetFeedback", sqlConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlConnection.Open();
                List<GetFeedbackModel> feedbackBLs = new List<GetFeedbackModel>();
                cmd.Parameters.AddWithValue("@bookId", bookId);
                var res = cmd.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        GetFeedbackModel get = new GetFeedbackModel();
                        get.FeedbackId = Convert.ToInt32(0);
                        get.Rating = Convert.ToInt32(1);
                        get.comment = (2).ToString();
                        get.bookId = Convert.ToInt32(3);
                        get.userId = Convert.ToInt32(4);
                        feedbackBLs.Add(get);
                    }
                    sqlConnection.Close();
                    return feedbackBLs;
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
