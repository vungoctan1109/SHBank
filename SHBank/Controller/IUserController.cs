using SHBank.Entity;

namespace SHBank.Controller
{
    public interface IUserController
    {
        Account Register();
        Account Login();
        void ShowInformation();
        void Withdraw(Account account);
        void Deposit(Account account);
        void Transfer(Account account);
        void CheckBalance(Account account);
        void UpdateInformation(Account account);
        void ChangePassword(Account account);
        void CheckTransactionHistory(Account account);
    }
}