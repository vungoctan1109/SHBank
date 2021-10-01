using System;
using MySql.Data.MySqlClient;
using SHBank.Entity;
using SHBank.Util;

namespace SHBank.Model
{
    public class AdminModel : IAdminModel
    {
        private readonly string _insertCommand =
            $"INSERT INTO admins (account_number, username, password_hash, salt, first_name, last_name, dob, gender, email, identity_number, phone, address, created_at, updated_at)" +
            $"VALUES (@account_number, @username, @password_hash, @salt, @first_name, @last_name, @dob, @gender, @email, @identity_number, @phone, @address, @created_at, @updated_at)";
        private readonly string _selectByAccountNumberCommand = $"SELECT * FROM admins WHERE account_number = @account_number";
        private readonly string _selectByUsernameCommand = $"SELECT * FROM admins WHERE username = @username";
        public Admin Save(Admin admin)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_insertCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@account_number", admin.AccountNumber);
                    mySqlCommand.Parameters.AddWithValue("@username", admin.Username);
                    mySqlCommand.Parameters.AddWithValue("@password_hash", admin.PasswordHash);
                    mySqlCommand.Parameters.AddWithValue("@salt", admin.Salt);
                    mySqlCommand.Parameters.AddWithValue("@first_name", admin.FirstName);
                    mySqlCommand.Parameters.AddWithValue("@last_name", admin.LastName);
                    mySqlCommand.Parameters.AddWithValue("@dob", admin.Dob);
                    mySqlCommand.Parameters.AddWithValue("@gender", admin.Gender);
                    mySqlCommand.Parameters.AddWithValue("@email", admin.Email);
                    mySqlCommand.Parameters.AddWithValue("@identity_number", admin.IdentityNumber);
                    mySqlCommand.Parameters.AddWithValue("@phone", admin.Phone);
                    mySqlCommand.Parameters.AddWithValue("@address", admin.Address);
                    mySqlCommand.Parameters.AddWithValue("@created_at", admin.CreatedAt);
                    mySqlCommand.Parameters.AddWithValue("@updated_at", admin.UpdatedAt);
                    mySqlCommand.Prepare();
                    var result = mySqlCommand.ExecuteNonQuery();
                    cnn.Close();
                    if (result > 0)
                    {
                        return admin;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public bool Update(string accountNumber, Admin updateAccount)
        {
            throw new System.NotImplementedException();
        }

        public Admin FindByUsername(string username)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_selectByUsernameCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@username", username);
                    mySqlCommand.Prepare();
                    using (var reader = mySqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var admin = new Admin()
                            {
                                AccountNumber = reader.GetString("account_number"),
                                Username = reader.GetString("username"),
                                PasswordHash = reader.GetString("password_hash"),
                                Salt = reader.GetString("salt"),
                                FirstName = reader.GetString("first_name"),
                                LastName = reader.GetString("last_name"),
                                Dob = reader.GetDateTime("dob"),
                                Gender = reader.GetInt32("gender"),
                                Email = reader.GetString("email"),
                                IdentityNumber = reader.GetString("identity_number"),
                                Phone = reader.GetString("phone"),
                                Address = reader.GetString("address"),
                                CreatedAt = reader.GetDateTime("created_at"),
                                UpdatedAt = reader.GetDateTime("updated_at"),
                            };
                            return admin;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

        public Admin FindByAccountNumber(string accountNumber)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_selectByAccountNumberCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@account_number", accountNumber);
                    mySqlCommand.Prepare();
                    using (var reader = mySqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var admin = new Admin()
                            {
                                AccountNumber = reader.GetString("account_number"),
                                Username = reader.GetString("username"),
                                PasswordHash = reader.GetString("password_hash"),
                                Salt = reader.GetString("salt"),
                                FirstName = reader.GetString("first_name"),
                                LastName = reader.GetString("last_name"),
                                Dob = reader.GetDateTime("dob"),
                                Gender = reader.GetInt32("gender"),
                                Email = reader.GetString("email"),
                                IdentityNumber = reader.GetString("identity_number"),
                                Phone = reader.GetString("phone"),
                                Address = reader.GetString("address"),
                                CreatedAt = reader.GetDateTime("created_at"),
                                UpdatedAt = reader.GetDateTime("updated_at"),
                            };
                            return admin;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }
    }
}