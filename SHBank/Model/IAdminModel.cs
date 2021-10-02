using SHBank.Entity;

namespace SHBank.Model
{
    public interface IAdminModel
    {
        Admin Save(Admin account);
        bool Update(Admin updateAccount);
        bool UpdatePassword(Admin account);
        Admin FindByUsername(string username);
        Admin FindByAccountNumber(string accountNumber);
    }
}