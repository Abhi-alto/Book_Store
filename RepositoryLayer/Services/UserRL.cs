using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using CommonLayer.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Services
{
    public class UserRL: IUserRL
    {
        private IConfiguration _config;
        const string connectionString = @"Server=.;Database=Book_Store;Trusted_Connection=True;";
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public UserRL(IConfiguration config)
        {
            this._config = config;
        }

        public void Register(UserRegisterModel userModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Register",sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@Name", userModel.Name);
                cmd.Parameters.AddWithValue("@Email", userModel.Email);
                cmd.Parameters.AddWithValue("@Password", userModel.Password);
                cmd.Parameters.AddWithValue("@Phone_no", userModel.Phone_Num);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch(Exception ex)
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
                    new Claim("Email", email)
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
