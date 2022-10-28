using CommonLayer.Admin;
using CommonLayer.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        private IConfiguration _config;
        const string connectionString = @"Server=.;Database=Book_Store;Trusted_Connection=True;";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public string LoginAdmin(AdminModel adminModel)
        {
                try
                {
                    SqlCommand cmd = new SqlCommand("AdminLogin", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    cmd.Parameters.AddWithValue("@Email", adminModel.Email);
                    cmd.Parameters.AddWithValue("@Password", adminModel.Password);
                    /*SqlDataAdapter adapter = new SqlDataAdapter();
                    DataTable dt = new DataTable();*/
                    //adapter.Fill(dt);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Close();
                        // var id = Convert.ToInt32(reader.GetInt32(0));
                        SqlCommand command = new SqlCommand("select AdminId from dbo.Admin where AdminEmail = @email", sqlConnection);
                        command.Parameters.AddWithValue("@Email", adminModel.Email);
                        var Id = Convert.ToInt32(command.ExecuteScalar());
                        sqlConnection.Close();
                        return GenerateJwtToken(adminModel.Email, Id);
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        private string GenerateJwtToken(string email, int UserId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("ThisIsMYSecretKey");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email),
                    new Claim("UserId",UserId.ToString()),
                    new Claim(ClaimTypes.Role,"Admin")
                    }),
                    Expires
                    = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                    new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GenerateToken(string email)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("ThisIsMYSecretKey");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email),
                    new Claim(ClaimTypes.Role,"Admin")
                    }),
                    Expires
                    = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                         new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
