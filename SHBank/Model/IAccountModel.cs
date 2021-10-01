using System;
using System.Collections.Generic;
using SHBank.Entity;

namespace SHBank.Model
{
    public interface IAccountModel
    {
        Account Save(Account account);
        bool Update(Account updateAccount);
        void UpdateAmount(Account account);
        bool UpdatePassword(Account account);
        bool Delete(int id);
        
        Account FindByAccountNumber(string accountNumber);
        Account FindByUsername(string username);
        List<Account> FindAll(int page, int limit);
        List<Account> SearchByName(string name, int page, int limit);
        List<Account> SearchByPhone(string phone, int page, int limit);

        List<TransactionHistory> FindTransactionHistory(
            string accountNumber,
            DateTime startTime,
            DateTime endTime);

        TransactionHistory Deposit(string accountNumber, double amount);
        TransactionHistory Withdraw(string accountNumber, double amount);

        TransactionHistory Transfer(
            string senderAccountNumber,
            string receiveAccountNumber,
            string message,
            double amount);
        
    }
}