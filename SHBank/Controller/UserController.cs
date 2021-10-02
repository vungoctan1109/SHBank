using System;
using System.Collections.Generic;
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

        public Account GetAccountInformation()
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
            if (existingAccount != null &&
                Hash.ComparePassword(account.Password, existingAccount.Salt, existingAccount.PasswordHash))
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

        public void Withdraw(Account account)
        {
            try
            {
                Console.WriteLine("Vui lòng nhập số tiền bạn muốn rút: ");
                var amount = Convert.ToDouble(Console.ReadLine());
                if (account.Balance > amount && amount > 0)
                {
                    account.Balance -= amount;
                    _accountModel.UpdateAmount(account);
                    Console.WriteLine("Rút tiền thành công. Số tiền trong tài khoản là {0}", account.Balance);
                    _accountModel.Withdraw(account.AccountNumber, amount);
                }
                else
                {
                    Console.WriteLine("Số tiền trong tài khoản không đủ để rút.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Số tiền quý khách nhập không hợp lệ.");
            }
        }

        public void Deposit(Account account)
        {
            try
            {
                Console.WriteLine("Vui lòng nhập số tiền bạn muốn gửi ngân hàng: ");
                var amount = Convert.ToDouble(Console.ReadLine());
                if (amount < 0)
                {
                    Console.WriteLine("Số tiền nhập không hợp lệ. Mong quý khách kiểm tra lại.");
                }
                else
                {
                    account.Balance += amount;
                    _accountModel.UpdateAmount(account);
                    Console.WriteLine("Gửi tiền thành công. Số tiền trong tài khoản là {0}", account.Balance);
                    _accountModel.Deposit(account.AccountNumber, amount);
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Số tiền quý khách nhập không hợp lệ.");
            }
        }

        public void Transfer(Account account)
        {
            try
            {
                Console.WriteLine("Vui lòng nhập số tài khoản cần chuyển tiền: ");
                var receiverAccountNumber = Console.ReadLine();
                var receiverAccount = _accountModel.FindByAccountNumber(receiverAccountNumber);
                if (receiverAccount != null)
                {
                    Console.WriteLine("Vui lòng nhập số tiền bạn cần gửi");
                    var amount = Convert.ToDouble(Console.ReadLine());
                    if (amount < 0)
                    {
                        Console.WriteLine("Số tiền bạn muốn gửi không hợp lệ.");
                    }
                    else if (account.Balance < amount)
                    {
                        Console.WriteLine("Số tiền trong tài khoản không đủ để chuyển khoản.");
                    }
                    else
                    {
                        Console.WriteLine("Bạn gửi tiền với lời nhắn là: ");
                        var message = Console.ReadLine();
                        account.Balance -= amount;
                        receiverAccount.Balance += amount;
                        _accountModel.UpdateAmount(account);
                        _accountModel.UpdateAmount(receiverAccount);
                        Console.WriteLine("Chuyển khoản thành công.");
                        Console.WriteLine("Số dư còn lại trong tài khoản là {0}", account.Balance);
                        _accountModel.Transfer(account.AccountNumber, receiverAccount.AccountNumber, message, amount);
                    }
                }
                else
                {
                    Console.WriteLine("Không tồn tại tài khoản với số tài khoản đã nhập.");
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Số tiền quý khách nhập không hợp lệ.");
            }
        }

        public void CheckBalance(Account account)
        {
            Console.WriteLine("Số dư trong tài khoản hiện tại là: {0}", account.Balance);
        }

        public void UpdateInformation(Account account)
        {
            var isValid = false;
            do
            {
                account = GetUpdateInformation(account);
                var errors = account.CheckValidUpdate();
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

            if (_accountModel.Update(account))
            {
                Console.WriteLine("Cập nhật thông tin thành công.");
            }
        }

        private Account GetUpdateInformation(Account account)
        {
            var isNotValidDob = true;
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

            Console.WriteLine("Vui lòng nhập email: ");
            account.Email = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập số điện thoại: ");
            account.Phone = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập địa chỉ");
            account.Address = Console.ReadLine();
            return account;
        }

        public void ChangePassword(Account account)
        {
            Console.WriteLine("Vui lòng nhập password hiện tại: ");
            var currentPassword = Console.ReadLine();
            if (Hash.ComparePassword(currentPassword, account.Salt, account.PasswordHash))
            {
                Console.WriteLine("Vui lòng nhập mật khẩu mới: ");
                var newPassword = Console.ReadLine();
                Console.Write("Vui lòng xác nhận mật khẩu: ");
                var newConfirmPassword = Console.ReadLine();
                if (newPassword == newConfirmPassword)
                {
                    account.Password = newPassword;
                    account.Salt = Hash.RandomString(7);
                    account.PasswordHash = Hash.GenerateSaltedSHA1(account.Password, account.Salt);
                    if (_accountModel.UpdatePassword(account))
                    {
                        Console.WriteLine("Đổi mật khẩu thành công.");
                    }
                }
                else
                {
                    Console.WriteLine("Mật khẩu đã nhập và xác nhận mật khẩu không trùng nhau.");
                }
            }
            else
            {
                Console.WriteLine("Bạn đã nhập sai mật khẩu. Vui lòng thử lại sau.");
            }
        }

        public void CheckTransactionHistory(Account account)
        {
            Console.WriteLine("Vui lòng nhập thời gian bắt đầu: ");
            var startTime = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Vui lòng nhập thời gian kết thúc: ");
            var endTime = Convert.ToDateTime(Console.ReadLine());
            var transactionHistories = _accountModel.FindTransactionHistory(account.AccountNumber, startTime, endTime);
            if (transactionHistories != null)
            {
                Console.WriteLine(String.Format("|{0,37}|{1,37}|{2,37}|{3,4}|{4,10}|{5,15}|{6,22}|{7,6}|", "ID", "Sender Account Number", "Receiver Account Number", "Type", "Amount", "Message", "Created At", "Status"));
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                foreach (var transactionHistory in transactionHistories)
                {
                    Console.WriteLine(transactionHistory.ToString());
                }    
            }
            else
            {
                Console.WriteLine("Không tồn tại giao dịch nào với thời gian như trên.");
            }
        }
    }
}