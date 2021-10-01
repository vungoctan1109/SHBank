using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SHBank.Entity;
using SHBank.Util;

namespace SHBank.Model
{
    public class AccountModel : IAccountModel
    {
        private readonly string _insertCommand =
            $"INSERT INTO accounts (account_number, balance, username, password_hash, salt, first_name, last_name, dob, gender, email, identity_number, phone, address, created_at, updated_at, deleted_at, status)" +
            $"VALUES (@account_number, @balance, @username, @password_hash, @salt, @first_name, @last_name, @dob, @gender, @email, @identity_number, @phone, @address, @created_at, @updated_at, @deleted_at, @status)";

        private readonly string _selectByAccountNumberCommand = $"SELECT * FROM accounts WHERE account_number = @account_number";
        private readonly string _selectByUsernameCommand = $"SELECT * FROM accounts WHERE username = @username";
        private readonly string _updateAmountCommand = $"UPDATE accounts SET balance = @balance, updated_at = @updated_at WHERE account_number = @account_number";
        private readonly string _updateInformationCommand = $"UPDATE accounts SET first_name = @first_name, last_name = @last_name, dob = @dob, email = @email, phone = @phone, address = @address, updated_at = @updated_at WHERE account_number = @account_number";
        private readonly string _updatePasswordCommand = $"UPDATE accounts SET password_hash = @password_hash, salt = @salt, updated_at = @updated_at WHERE account_number = @account_number";

        public Account Save(Account account)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_insertCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@account_number", account.AccountNumber);
                    mySqlCommand.Parameters.AddWithValue("@balance", account.Balance);
                    mySqlCommand.Parameters.AddWithValue("@username", account.Username);
                    mySqlCommand.Parameters.AddWithValue("@password_hash", account.PasswordHash);
                    mySqlCommand.Parameters.AddWithValue("@salt", account.Salt);
                    mySqlCommand.Parameters.AddWithValue("@first_name", account.FirstName);
                    mySqlCommand.Parameters.AddWithValue("@last_name", account.LastName);
                    mySqlCommand.Parameters.AddWithValue("@dob", account.Dob);
                    mySqlCommand.Parameters.AddWithValue("@gender", account.Gender);
                    mySqlCommand.Parameters.AddWithValue("@email", account.Email);
                    mySqlCommand.Parameters.AddWithValue("@identity_number", account.IdentityNumber);
                    mySqlCommand.Parameters.AddWithValue("@phone", account.Phone);
                    mySqlCommand.Parameters.AddWithValue("@address", account.Address);
                    mySqlCommand.Parameters.AddWithValue("@created_at", account.CreatedAt);
                    mySqlCommand.Parameters.AddWithValue("@updated_at", account.UpdatedAt);
                    mySqlCommand.Parameters.AddWithValue("@deleted_at", account.DeletedAt);
                    mySqlCommand.Parameters.AddWithValue("@status", account.Status);
                    mySqlCommand.Prepare();
                    var result = mySqlCommand.ExecuteNonQuery();
                    cnn.Close();
                    if (result > 0)
                    {
                        return account;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public bool Update(Account updateAccount)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_updateInformationCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@first_name", updateAccount.FirstName);
                    mySqlCommand.Parameters.AddWithValue("@last_name", updateAccount.LastName);
                    mySqlCommand.Parameters.AddWithValue("@dob", updateAccount.Dob);
                    mySqlCommand.Parameters.AddWithValue("@email", updateAccount.Email);
                    mySqlCommand.Parameters.AddWithValue("@phone", updateAccount.Phone);
                    mySqlCommand.Parameters.AddWithValue("@address", updateAccount.Address);
                    mySqlCommand.Parameters.AddWithValue("@updated_at", DateTime.Now);
                    mySqlCommand.Parameters.AddWithValue("@account_number", updateAccount.AccountNumber);
                    mySqlCommand.Prepare();
                    mySqlCommand.ExecuteNonQuery();
                    cnn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return true;
        }
        
        public void UpdateAmount(Account account)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    account.UpdatedAt = DateTime.Now;
                    var mySqlCommand = new MySqlCommand(_updateAmountCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@balance", account.Balance);
                    mySqlCommand.Parameters.AddWithValue("@updated_at", account.UpdatedAt);
                    mySqlCommand.Parameters.AddWithValue("@account_number", account.AccountNumber);
                    mySqlCommand.Prepare();
                    mySqlCommand.ExecuteNonQuery();
                    cnn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public bool UpdatePassword(Account account)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    account.UpdatedAt = DateTime.Now;
                    var mySqlCommand = new MySqlCommand(_updatePasswordCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@password_hash", account.PasswordHash);
                    mySqlCommand.Parameters.AddWithValue("@salt", account.Salt);
                    mySqlCommand.Parameters.AddWithValue("@updated_at", account.UpdatedAt);
                    mySqlCommand.Parameters.AddWithValue("@account_number", account.AccountNumber);
                    mySqlCommand.Prepare();
                    mySqlCommand.ExecuteNonQuery();
                    cnn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Account FindByAccountNumber(string accountNumber)
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
                            var account = new Account()
                            {
                                AccountNumber = reader.GetString("account_number"),
                                Balance = reader.GetDouble("balance"),
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
                                DeletedAt = reader.GetDateTime("deleted_at"),
                                Status = reader.GetInt32("status")
                            };
                            return account;
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

        public Account FindByUsername(string username)
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
                             var account = new Account()
                             {
                                 AccountNumber = reader.GetString("account_number"),
                                 Balance = reader.GetDouble("balance"),
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
                                 DeletedAt = reader.GetDateTime("deleted_at"),
                                 Status = reader.GetInt32("status")
                             };
                             return account;
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

        public List<Account> FindAll(int page, int limit)
        {
            throw new NotImplementedException();
        }

        public List<Account> SearchByName(string name, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public List<Account> SearchByPhone(string phone, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public List<TransactionHistory> FindTransactionHistory(string accountNumber, DateTime startTime,
            DateTime endTime, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public TransactionHistory Deposit(string accountNumber, double amount)
        {
            throw new NotImplementedException();
        }

        public TransactionHistory Withdraw(string accountNumber, double amount)
        {
            throw new NotImplementedException();
        }

        public TransactionHistory Transfer(string senderAccountNumber, string receiveAccountNumber, string message,
            double amount)
        {
            
            throw new NotImplementedException();
        }
    }
}