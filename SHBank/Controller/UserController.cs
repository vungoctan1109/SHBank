using System;
using System.Globalization;
using SHBank.Entity;
using SHBank.Model;
using SHBank.Util;
using SHBank.View;

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
                if (CheckExistUsername(account.Username))
                {
                    errors.Add("username_duplicate", "Username đã tồn tại.");
                }
                isValid = errors.Count == 0;
                if (!isValid)
                {
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.Value);
                    }

                    Console.WriteLine("Vui lòng nhập lại thông tin tài khoản.");
                    Console.ReadLine();
                }
            } while (!isValid);

            while (CheckExistAccountNumber(account.AccountNumber))
            {
                account.GenerateAccountNumber();
            }
            Console.WriteLine(account.ToString());
            account.EncryptPassword();
            var result = _accountModel.Save(account);
            if (result == null) return null;
            Console.WriteLine("Đăng ký thành công.");
            return result;
        }

        private bool CheckExistUsername(string username)
        {
            return _accountModel.FindByUsername(username) != null;
        }

        private bool CheckExistAccountNumber(string accountNumber)
        {
            return _accountModel.FindByAccountNumber(accountNumber) != null;
        }

        private Account GetAccountInformation()
        {
            Console.Clear();
            var isNotValidDob = true;
            var isNotValidBalance = true;
            var isNotValidGender = true;
            var account = new Account();
            Console.WriteLine("Vui lòng nhập username: ");
            account.Username = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập password: ");
            account.Password = Console.ReadLine();
            Console.WriteLine("Vui lòng xác nhận lại password: ");
            account.ConfirmPassword = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập số tiền bạn muốn gửi vào ngân hàng: ");
            while (isNotValidBalance)
            {
                try
                {
                    account.Balance = Convert.ToDouble(Console.ReadLine());
                    isNotValidBalance = false;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Vui lòng nhập lại số tiền bạn muốn gửi vào ngân hàng");
                }
            }

            Console.WriteLine("Vui lòng nhập tên của bạn: ");
            account.FirstName = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập họ và tên đệm của bạn: ");
            account.LastName = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập ngày sinh theo format mm/dd/yyyy");
            while (isNotValidDob)
            {
                try
                {
                    var inputDob = Console.ReadLine();
                    account.Dob = DateTime.ParseExact(inputDob, "d", CultureInfo.InvariantCulture);
                    isNotValidDob = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Vui lòng nhập lại đúng format mm/dd/yyyy.");
                }
            }
            
            Console.WriteLine("Vui lòng nhập giới tính của bạn: 1. Nam   2. Nữ");
            while (isNotValidGender)
            {
                try
                {
                    account.Gender = Convert.ToInt32(Console.ReadLine());
                    isNotValidGender = false;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Hãy nhập số 1 hoặc 2.");
                }
            }
            Console.WriteLine("Vui lòng nhập email: ");
            account.Email = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập số CMND/CCCD: ");
            account.IdentityNumber = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập số điện thoại: ");
            account.Phone = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập địa chỉ");
            account.Address = Console.ReadLine();
            return account;
        }

        public Account Login()
        {
            Account account = null;
            var isValid = false;
            do
            {
                account = GetLoginInformation();
                var errors = account.CheckValidLogin();
                isValid = errors.Count == 0;
                if (!isValid)
                {
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.Value);
                    }

                    Console.WriteLine("Vui lòng nhập lại thông tin tài khoản đăng nhập.");
                    Console.ReadLine();
                }
            } while (!isValid);

            Account existingAccount = _accountModel.FindByUsername(account.Username);
            if (existingAccount != null && Hash.ComparePassword(account.Password, existingAccount.Salt, existingAccount.PasswordHash))
            {
                Console.WriteLine("Đăng nhập thành công.");
                UserView userView = new UserView();
                userView.GenerateUserView(existingAccount);
            }
            return null;
        }

        private Account GetLoginInformation()
        {
            var account = new Account();
            Console.WriteLine("Vui lòng nhập username: ");
            account.Username = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập mật khẩu: ");
            account.Password = Console.ReadLine();
            return account;
        }

        public void ShowInformation()
        {
            throw new System.NotImplementedException();
        }

        public void Withdraw()
        {
            Console.WriteLine("Something.");
            throw new System.NotImplementedException();
        }

        public void Deposit()
        {
            throw new System.NotImplementedException();
        }

        public void Transfer( Account account)
        {
            Console.WriteLine("Vui lòng nhập số tài khoản cần chuyển tiền: ");
            var receiverAccountNumber = Console.ReadLine();
            var receiverAccount = _accountModel.FindByAccountNumber(receiverAccountNumber);
            if (receiverAccount != null)
            {
                Console.WriteLine("Vui lòng nhập số tiền bạn cần gửi");
                var money = Convert.ToDouble(Console.ReadLine());
                if (money < 0 || money.Equals(null))
                {
                    Console.WriteLine("Số tiền bạn muốn gửi không hợp lệ.");
                }
                else if (account.Balance < money)
                {
                    Console.WriteLine("Số tiền trong tài khoản không đủ để chuyển khoản.");
                }
                else
                {
                    
                    Console.WriteLine("Chuyển khoản thành công.");
                    Console.WriteLine("Số dư còn lại trong tài khoản là {0}", account.Balance);
                }
            }
            else
            {
                Console.WriteLine("Không tồn tại tài khoản với số tài khoản đã nhập.");
            }

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