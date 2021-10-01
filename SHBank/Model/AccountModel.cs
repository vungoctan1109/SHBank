using System;
using System.Collections.Generic;
using SHBank.Entity;

namespace SHBank.Model
{
    public class AccountModel : IAccountModel
    {
        public Account Save(Account account)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Account updateAccount)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Account FindByAccountNumber(int id)
        {
            throw new NotImplementedException();
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

        public List<TransactionHistory> FindTransactionHistory(string accountNumber, DateTime startTime, DateTime endTime, int page, int limit)
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

        public TransactionHistory Transfer(string senderAccountNumber, string receiveAccountNumber, string message, double amount)
        {
            throw new NotImplementedException();
        }
    }
}