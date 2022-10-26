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
                cmd.Parameters.AddWithValue("@name", userModel.Name);
                cmd.Parameters.AddWithValue("@email", userModel.Email);
                cmd.Parameters.AddWithValue("@password", userModel.Password);
                cmd.Parameters.AddWithValue("@phone", userModel.Phone_Num);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }   
        public string LoginUser(LoginModel loginModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Login", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@Email", loginModel.Email);
                cmd.Parameters.AddWithValue("@Password", loginModel.Password);
                /*SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dt = new DataTable();*/
                //adapter.Fill(dt);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    // var id = Convert.ToInt32(reader.GetInt32(0));
                    SqlCommand command = new SqlCommand("select Id from dbo.Users where email = @email", sqlConnection);
                    command.Parameters.AddWithValue("@email", loginModel.Email);
                    var Id = Convert.ToInt32(command.ExecuteScalar());
                    sqlConnection.Close();
                    return GenerateJwtToken(loginModel.Email,Id);
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
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();

            }
            catch (MessageQueueException ex)

            {

                if (ex.MessageQueueErrorCode ==

                MessageQueueErrorCode.AccessDenied)

                {
                    Console.WriteLine("Access is denied. " +
                    "Queue might be a system queue.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgetPassword(String email)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ForgotPassword", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                cmd.Parameters.AddWithValue("@Email", email);
                MessageQueue bookStore = new MessageQueue();
                var val = cmd.ExecuteScalar();
                if (val == null)
                {
                    return false;
                }
                SqlCommand comm = new SqlCommand("select Id from dbo.Users where email = @email", sqlConnection);
                comm.Parameters.AddWithValue("@email", email);
                var id = Convert.ToInt32(comm.ExecuteScalar());

                //Setting the QueuPath where we want to store the messages.
                bookStore.Path = @".\private$\FunDo_Notes";
                if (MessageQueue.Exists(bookStore.Path))
                {
                    //Exists
                    bookStore = new MessageQueue(@".\Private$\FunDo_Notes");
                }
                else
                {
                    // Creates the new queue named "Bills"
                    MessageQueue.Create(bookStore.Path);
                }
                Message MyMessage = new Message();
                MyMessage.Formatter = new BinaryMessageFormatter();
                MyMessage.Body = GenerateJwtToken(email, id);
                MyMessage.Label = "Forget Password Email";
                bookStore.Send(MyMessage);
                Message msg = bookStore.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendEmail(email, msg.Body.ToString());
                bookStore.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                bookStore.BeginReceive();
                bookStore.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(string email, PasswordModel passwordModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ForgotPassword", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
            
                SqlCommand comm = new SqlCommand("select Id from dbo.Users where email = @email", sqlConnection);
                cmd.Parameters.AddWithValue("@Email", email);
                var id = comm.ExecuteScalar();
                var newId = Convert.ToInt32(id);

                cmd.Parameters.AddWithValue("@password", passwordModel.NewPassword);
                //cmd.Parameters.AddWithValue("@userId", newId);
                var val = cmd.ExecuteNonQuery();
                if (passwordModel.NewPassword != passwordModel.CPassword)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
