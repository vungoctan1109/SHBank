using System;
using SHBank.Entity;
using SHBank.Model;
using SHBank.Util;

namespace SHBank.Controller
{
    public class UserController : IUserController
    {
        private IAccountModel _accountModel;

        public UserController()
        {
            _accountModel = new AccountModel();
        }

        public Account Register()
        {
            Account account = null;
            var isValid = false;
            do
            {
                account = GetAccountInformation();
                var errors = account.IsValid();
                isValid = errors.Count == 0;
                if (!isValid)
                {
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.Key + ", " + error.Value);
                    }

                    Console.WriteLine("Vui lòng nhập lại thông tin tài khoản.");
                    Console.ReadLine();
                }
            } while (!isValid);

            Console.WriteLine(account.ToString());
            account.EncryptPassword();
            var result = _accountModel.Save(account);
            if (result != null)
            {
                Console.WriteLine("Đăng ký thành công.");
                return result;
            }
            return account;
        }

        private Account GetAccountInformation()
        {
            Console.Clear();
            var account = new Account();
            
            return account;
        }

        public Account Login()
        {
            throw new System.NotImplementedException();
        }

        public void ShowInformation()
        {
            throw new System.NotImplementedException();
        }

        public void Withdraw()
        {
            throw new System.NotImplementedException();
        }

        public void Deposit()
        {
            throw new System.NotImplementedException();
        }

        public void Transfer()
        {
            throw new System.NotImplementedException();
        }

        public void CheckInformation()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateInformation()
        {
            throw new System.NotImplementedException();
        }

        public void ChangePassword()
        {
            throw new System.NotImplementedException();
        }

        public void CheckTransactionHistory()
        {
            throw new System.NotImplementedException();
        }
    }
}