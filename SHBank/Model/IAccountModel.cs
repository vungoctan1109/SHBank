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
        bool LockAccount(string accountNumber);
        bool UnlockAccount(string accountNumber);
        Account FindByAccountNumber(string accountNumber);
        Account FindByUsername(string username);
        List<Account> FindAll();
        List<Account> SearchByName(string name);
        List<Account> SearchByPhone(string phone);
        List<TransactionHistory> FindAllTransactionHistories();
        List<TransactionHistory> FindTransactionHistory(
            string accountNumber,
            DateTime startTime,
            DateTime endTime);
        List<TransactionHistory> FindAllTransactionHistoriesByAccountNumber(string accountNumber);
        TransactionHistory Deposit(string accountNumber, double amount);
        TransactionHistory Withdraw(string accountNumber, double amount);

        TransactionHistory Transfer(
            string senderAccountNumber,
            string receiveAccountNumber,
            string message,
            double amount);
        
    }
}