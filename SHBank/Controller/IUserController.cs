using SHBank.Entity;

namespace SHBank.Controller
{
    public interface IUserController
    {
        Account Register();
        Account Login();
        void ShowInformation();
        void Withdraw();
        void Deposit();
        void Transfer();
        void CheckInformation();
        void UpdateInformation();
        void ChangePassword();
        void CheckTransactionHistory();
    }
}