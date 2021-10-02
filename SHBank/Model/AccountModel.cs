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

        private readonly string _transactionCommand =
            $"INSERT INTO transaction_history(id, sender, receiver, type, amount, message, created_at, status) VALUES (@id, @sender, @receiver, @type, @amount, @message, @created_at, @status)";

        private readonly string _selectByAccountNumberCommand =
            $"SELECT * FROM accounts WHERE account_number = @account_number";

        private readonly string _selectByUsernameCommand = $"SELECT * FROM accounts WHERE username = @username";
        private readonly string _selectUserCommand = $"SELECT * FROM accounts";

        private readonly string _selectAccountByNameCommand =
            $"SELECT * FROM accounts WHERE first_name LIKE @first_name";

        private readonly string _selectAccountByPhoneCommand = $"SELECT * FROM accounts WHERE phone LIKE @phone";

        private readonly string _updateAmountCommand =
            $"UPDATE accounts SET balance = @balance, updated_at = @updated_at WHERE account_number = @account_number";

        private readonly string _updateInformationCommand =
            $"UPDATE accounts SET first_name = @first_name, last_name = @last_name, dob = @dob, email = @email, phone = @phone, address = @address, updated_at = @updated_at WHERE account_number = @account_number";

        private readonly string _updatePasswordCommand =
            $"UPDATE accounts SET password_hash = @password_hash, salt = @salt, updated_at = @updated_at WHERE account_number = @account_number";
        private readonly string _updateStatusCommand =
            $"UPDATE accounts SET status = @status WHERE account_number = @account_number";

        private readonly string _selectTransactionCommand =
            $"SELECT * FROM transaction_history WHERE sender = @sender OR receiver = @receiver";

        private readonly string _selectTransactionHistoryCommand = $"SELECT * FROM transaction_history";

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
                    var mySqlCommand = new MySqlCommand(_updateAmountCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@balance", account.Balance);
                    mySqlCommand.Parameters.AddWithValue("@updated_at", DateTime.Now);
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

        public bool LockAccount(string accountNumber)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_updateStatusCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@status", -1);
                    mySqlCommand.Parameters.AddWithValue("@account_number", accountNumber);
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

        public bool UnlockAccount(string accountNumber)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_updateStatusCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@status", 1);
                    mySqlCommand.Parameters.AddWithValue("@account_number", accountNumber);
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

        public List<Account> FindAll()
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_selectUserCommand, cnn);
                    mySqlCommand.Prepare();
                    List<Account> accounts = new List<Account>();
                    using (var reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var account = new Account()
                            {
                                AccountNumber = reader.GetString("account_number"),
                                Balance = reader.GetDouble("balance"),
                                Username = reader.GetString("username"),
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
                            accounts.Add(account);
                        }

                        return accounts;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Account> SearchByName(string name)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_selectAccountByNameCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@first_name", "%" + name + "%");
                    mySqlCommand.Prepare();
                    List<Account> accounts = new List<Account>();
                    using (var reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var account = new Account()
                            {
                                AccountNumber = reader.GetString("account_number"),
                                Balance = reader.GetDouble("balance"),
                                Username = reader.GetString("username"),
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
                            accounts.Add(account);
                        }

                        return accounts;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Account> SearchByPhone(string phone)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_selectAccountByPhoneCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@phone", "%" + phone + "%");
                    mySqlCommand.Prepare();
                    List<Account> accounts = new List<Account>();
                    using (var reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var account = new Account()
                            {
                                AccountNumber = reader.GetString("account_number"),
                                Balance = reader.GetDouble("balance"),
                                Username = reader.GetString("username"),
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
                            accounts.Add(account);
                        }

                        return accounts;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<TransactionHistory> FindAllTransactionHistories()
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_selectTransactionHistoryCommand, cnn);
                    mySqlCommand.Prepare();
                    List<TransactionHistory> transactionHistories = new List<TransactionHistory>();
                    using (var reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var transactionHistory = new TransactionHistory()
                            {
                                Id = reader.GetString("id"),
                                SenderAccountNumber = reader.GetString("sender"),
                                ReceiverAccountNumber = reader.GetString("receiver"),
                                Type = reader.GetInt32("type"),
                                Amount = reader.GetDouble("amount"),
                                Message = reader.GetString("message"),
                                CreatedAt = reader.GetDateTime("created_at"),
                                Status = reader.GetInt32("status")
                            };
                            transactionHistories.Add(transactionHistory);
                        }

                        return transactionHistories;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public List<TransactionHistory> FindTransactionHistory(string accountNumber, DateTime startTime,
            DateTime endTime)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_selectTransactionCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@sender", accountNumber);
                    mySqlCommand.Parameters.AddWithValue("@receiver", accountNumber);
                    mySqlCommand.Prepare();
                    List<TransactionHistory> transactionHistories = new List<TransactionHistory>();
                    using (var reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var transactionHistory = new TransactionHistory()
                            {
                                Id = reader.GetString("id"),
                                SenderAccountNumber = reader.GetString("sender"),
                                ReceiverAccountNumber = reader.GetString("receiver"),
                                Type = reader.GetInt32("type"),
                                Amount = reader.GetDouble("amount"),
                                Message = reader.GetString("message"),
                                CreatedAt = reader.GetDateTime("created_at"),
                                Status = reader.GetInt32("status")
                            };
                            if (DateTime.Compare(startTime, transactionHistory.CreatedAt) <= 0 &&
                                DateTime.Compare(endTime, transactionHistory.CreatedAt) >= 0)
                            {
                                transactionHistories.Add(transactionHistory);
                            }
                        }

                        return transactionHistories;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<TransactionHistory> FindAllTransactionHistoriesByAccountNumber(string accountNumber)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_selectTransactionCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@sender", accountNumber);
                    mySqlCommand.Parameters.AddWithValue("@receiver", accountNumber);
                    mySqlCommand.Prepare();
                    List<TransactionHistory> transactionHistories = new List<TransactionHistory>();
                    using (var reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var transactionHistory = new TransactionHistory()
                            {
                                Id = reader.GetString("id"),
                                SenderAccountNumber = reader.GetString("sender"),
                                ReceiverAccountNumber = reader.GetString("receiver"),
                                Type = reader.GetInt32("type"),
                                Amount = reader.GetDouble("amount"),
                                Message = reader.GetString("message"),
                                CreatedAt = reader.GetDateTime("created_at"),
                                Status = reader.GetInt32("status")
                            };

                            transactionHistories.Add(transactionHistory);
                        }

                        return transactionHistories;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public TransactionHistory Deposit(string accountNumber, double amount)
        {
            try
            {
                var transactionHistory = new TransactionHistory();
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_transactionCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@id", transactionHistory.Id);
                    mySqlCommand.Parameters.AddWithValue("@sender", accountNumber);
                    mySqlCommand.Parameters.AddWithValue("@receiver", accountNumber);
                    mySqlCommand.Parameters.AddWithValue("@type", 2);
                    mySqlCommand.Parameters.AddWithValue("@amount", amount);
                    mySqlCommand.Parameters.AddWithValue("@message", " ");
                    mySqlCommand.Parameters.AddWithValue("@created_at", transactionHistory.CreatedAt);
                    mySqlCommand.Parameters.AddWithValue("@status", 1);
                    mySqlCommand.Prepare();
                    mySqlCommand.ExecuteNonQuery();
                    cnn.Close();
                    return transactionHistory;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public TransactionHistory Withdraw(string accountNumber, double amount)
        {
            try
            {
                var transactionHistory = new TransactionHistory();
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_transactionCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@id", transactionHistory.Id);
                    mySqlCommand.Parameters.AddWithValue("@sender", accountNumber);
                    mySqlCommand.Parameters.AddWithValue("@receiver", accountNumber);
                    mySqlCommand.Parameters.AddWithValue("@type", 1);
                    mySqlCommand.Parameters.AddWithValue("@amount", amount);
                    mySqlCommand.Parameters.AddWithValue("@message", " ");
                    mySqlCommand.Parameters.AddWithValue("@created_at", transactionHistory.CreatedAt);
                    mySqlCommand.Parameters.AddWithValue("@status", 1);
                    mySqlCommand.Prepare();
                    mySqlCommand.ExecuteNonQuery();
                    cnn.Close();
                    return transactionHistory;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public TransactionHistory Transfer(string senderAccountNumber, string receiveAccountNumber, string message,
            double amount)
        {
            try
            {
                var transactionHistory = new TransactionHistory();
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    cnn.Open();
                    var mySqlCommand = new MySqlCommand(_transactionCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@id", transactionHistory.Id);
                    mySqlCommand.Parameters.AddWithValue("@sender", senderAccountNumber);
                    mySqlCommand.Parameters.AddWithValue("@receiver", receiveAccountNumber);
                    mySqlCommand.Parameters.AddWithValue("@type", 3);
                    mySqlCommand.Parameters.AddWithValue("@amount", amount);
                    mySqlCommand.Parameters.AddWithValue("@message", message);
                    mySqlCommand.Parameters.AddWithValue("@created_at", transactionHistory.CreatedAt);
                    mySqlCommand.Parameters.AddWithValue("@status", 1);
                    mySqlCommand.Prepare();
                    mySqlCommand.ExecuteNonQuery();
                    cnn.Close();
                    return transactionHistory;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}