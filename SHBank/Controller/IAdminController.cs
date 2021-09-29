using SHBank.Entity;

namespace SHBank.Controller
{
    public interface IAdminController
    {
        Admin Register();
        Admin Login();
        void ShowListUser();
        void ShowListTransaction();
        void FindUserByAccountNumber();
        void FindUserByName();
        void FindUserByPhone();
        void CreateNewUser();
        void LockUser();
        void UnlockUser();
        void SearchTransactionHistory();
        void UpdateInformation();
        void ChangePassword();
    }
}