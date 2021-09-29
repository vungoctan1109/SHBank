using System;
using System.Collections.Generic;
using SHBank.Entity;

namespace SHBank.Model
{
    public interface IAccountModel
    {
        bool Save(Account account);
        bool Update(int id, Account updateAccount);
        bool Delete(int id);
        
        Account FindByAccountNumber(int id);
        List<Account> FindAll(int page, int limit);
        List<Account> SearchByName(string name, int page, int limit);
        List<Account> SearchByPhone(string phone, int page, int limit);

        List<TransactionHistory> FindTransactionHistory(
            string accountNumber,
            DateTime startTime,
            DateTime endTime,
            int page, int limit);

        TransactionHistory Deposit(string accountNumber, double amount);
        TransactionHistory Withdraw(string accountNumber, double amount);

        TransactionHistory Transfer(
            string senderAccountNumber,
            string receiveAccountNumber,
            string message,
            double amount);
        
    }
}