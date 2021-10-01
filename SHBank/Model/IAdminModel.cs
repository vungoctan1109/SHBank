using SHBank.Entity;

namespace SHBank.Model
{
    public interface IAdminModel
    {
        Admin Save(Admin account);
        bool Update(string accountNumber, Admin updateAccount);
        Admin FindByUsername(string username);
        Admin FindByAccountNumber(string accountNumber);
    }
}