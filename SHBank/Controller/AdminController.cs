using System;
using System.Collections.Generic;
using System.Globalization;
using SHBank.Entity;
using SHBank.Model;
using SHBank.Util;
using SHBank.View;

namespace SHBank.Controller
{
    public class AdminController : IAdminController
    {
        private IAdminModel _adminModel;
        private IAccountModel _accountModel;

        public AdminController()
        {
            _adminModel = new AdminModel();
            _accountModel = new AccountModel();
        }
        public Admin Register()
        {
            Admin admin = null;
            var isValid = false;
            do
            {
                admin = GetAccountAdminInformation();
                var errors = admin.IsValid();
                if (CheckExistUsername(admin.Username))
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

            while (CheckExistAccountNumber(admin.AccountNumber))
            {
                admin.GenerateAccountNumber();
            }
            Console.WriteLine(admin.ToString());
            admin.EncryptPassword();
            var result = _adminModel.Save(admin);
            if (result == null) return null;
            Console.WriteLine("Đăng ký thành công.");
            return result;
        }
        
        private bool CheckExistUsername(string username)
        {
            return _adminModel.FindByUsername(username) != null;
        }

        private bool CheckExistAccountNumber(string accountNumber)
        {
            return _adminModel.FindByAccountNumber(accountNumber) != null;
        }

        private Admin GetAccountAdminInformation()
        {
            Console.Clear();
            var isNotValidDob = true;
            var isNotValidGender = true;
            var admin = new Admin();
            Console.WriteLine("Vui lòng nhập username: ");
            admin.Username = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập password: ");
            admin.Password = Console.ReadLine();
            Console.WriteLine("Vui lòng xác nhận lại password: ");
            admin.ConfirmPassword = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập tên của bạn: ");
            admin.FirstName = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập họ và tên đệm của bạn: ");
            admin.LastName = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập ngày sinh theo format mm/dd/yyyy");
            while (isNotValidDob)
            {
                try
                {
                    var inputDob = Console.ReadLine();
                    admin.Dob = DateTime.ParseExact(inputDob, "d", CultureInfo.InvariantCulture);
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
                    admin.Gender = Convert.ToInt32(Console.ReadLine());
                    isNotValidGender = false;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Hãy nhập số 1 hoặc 2.");
                }
            }
            Console.WriteLine("Vui lòng nhập email: ");
            admin.Email = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập số CMND/CCCD: ");
            admin.IdentityNumber = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập số điện thoại: ");
            admin.Phone = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập địa chỉ");
            admin.Address = Console.ReadLine();
            return admin;
        }

        public Admin Login()
        {
            Admin admin = null;
            var isValid = false;
            do
            {
                admin = GetLoginInformation();
                var errors = admin.CheckValidLogin();
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

            Admin existingAccount = _adminModel.FindByUsername(admin.Username);
            if (existingAccount != null && Hash.ComparePassword(admin.Password, existingAccount.Salt, existingAccount.PasswordHash))
            {
                Console.WriteLine("Đăng nhập thành công.");
                AdminView userView = new AdminView();
                userView.GenerateAdminView(existingAccount);
            }
            else
            {
                Console.WriteLine("Mật khẩu tài khoản sai.");
            }
            return null;
        }
        private Admin GetLoginInformation()
        {
            var admin = new Admin();
            Console.WriteLine("Vui lòng nhập username: ");
            admin.Username = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập mật khẩu: ");
            admin.Password = Console.ReadLine();
            return admin;
        }

        public void ShowListUser()
        {
            var accounts = _accountModel.FindAll();
            Console.WriteLine(String.Format("|{0,37}|{1,13}|{2,10}|{3,10}|{4,10}|{5,22}|{6,6}|{7,15}|{8,12}|{9,11}|{10,15}",
                "AccountNumber", "Balance", "Username", "FirstName", "LastName", "Dob", "Gender", "Email", "IdentityNumber", "Phone", "Address"));
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (var t in accounts)
            {
                Console.WriteLine(t.ToString());
            }
        }

        public void ShowListTransaction()
        {
           var list = _accountModel.FindAllTransactionHistories();
           Console.WriteLine(String.Format("|{0,37}|{1,37}|{2,37}|{3,4}|{4,10}|{5,15}|{6,22}|{7,6}|", "ID", "Sender Account Number", "Receiver Account Number", "Type", "Amount", "Message", "Created At", "Status"));
           Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
           foreach (var t in list)
           {
               Console.WriteLine(t.ToString());
           }
        }

        public void FindUserByAccountNumber()
        {
            Console.WriteLine("Vui lòng nhập số tài khoản cần tìm: ");
            var accountNumber = Console.ReadLine();
            var account = _accountModel.FindByAccountNumber(accountNumber);
            if (account != null)
            {
                Console.WriteLine(String.Format("|{0,37}|{1,13}|{2,10}|{3,10}|{4,10}|{5,22}|{6,6}|{7,15}|{8,12}|{9,11}|{10,15}",
                    "AccountNumber", "Balance", "Username", "FirstName", "LastName", "Dob", "Gender", "Email", "IdentityNumber", "Phone", "Address"));
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine(account.ToString());    
            }
            else
            {
                Console.WriteLine("Không tìm thấy tài khoản với số tài khoản trên.");
            }
        }

        public void FindUserByName()
        {
            Console.WriteLine("Vui lòng nhập tên cần tìm: ");
            var name = Console.ReadLine();
            var list = _accountModel.SearchByName(name);
            if (list != null)
            {
                Console.WriteLine(String.Format("|{0,37}|{1,13}|{2,10}|{3,10}|{4,10}|{5,22}|{6,6}|{7,15}|{8,12}|{9,11}|{10,15}",
                    "AccountNumber", "Balance", "Username", "FirstName", "LastName", "Dob", "Gender", "Email", "IdentityNumber", "Phone", "Address"));
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                foreach (var t in list)
                {
                    Console.WriteLine(t.ToString());
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy tài khoản với tên như trên.");
            }
        }

        public void FindUserByPhone()
        {
            Console.WriteLine("Vui lòng nhập số điện thoại cần tìm: ");
            var phone = Console.ReadLine();
            var list = _accountModel.SearchByPhone(phone);
            if (list != null)
            {
                Console.WriteLine(String.Format("|{0,37}|{1,13}|{2,10}|{3,10}|{4,10}|{5,22}|{6,6}|{7,15}|{8,12}|{9,11}|{10,15}",
                    "AccountNumber", "Balance", "Username", "FirstName", "LastName", "Dob", "Gender", "Email", "IdentityNumber", "Phone", "Address"));
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                foreach (var t in list)
                {
                    Console.WriteLine(t.ToString());
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy tài khoản với số điện thoại như trên.");
            }
        }

        public void CreateNewUser()
        {
            Account account = null;
            var isValid = false;
            do
            {
                account = GetAccountInformation();
                var errors = account.IsValid();
                if (CheckExistUsernameUser(account.Username))
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

            while (CheckExistAccountNumberUser(account.AccountNumber))
            {
                account.GenerateAccountNumber();
            }

            Console.WriteLine(account.ToString());
            account.EncryptPassword();
            var result = _accountModel.Save(account);
            Console.WriteLine("Đăng ký thành công.");
        }
        private bool CheckExistUsernameUser(string username)
        {
            return _accountModel.FindByUsername(username) != null;
        }

        private bool CheckExistAccountNumberUser(string accountNumber)
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

        public void LockUser()
        {
            Console.WriteLine("Nhập số tài khoản cần khóa: ");
            var accountNumber = Console.ReadLine();
            var account = _accountModel.FindByAccountNumber(accountNumber);
            if (account != null && account.Status == 1)
            {
                _accountModel.LockAccount(accountNumber);
                Console.WriteLine("Thao tác thành công");
            }
            else
            {
                Console.WriteLine("Tài khoản đã bị khóa hoặc không tồn tại");
            }
            
        }

        public void UnlockUser()
        {
            Console.WriteLine("Nhập số tài khoản cần mở khóa: ");
            var accountNumber = Console.ReadLine();
            var account = _accountModel.FindByAccountNumber(accountNumber);
            if (account != null && account.Status == -1)
            {
                _accountModel.UnlockAccount(accountNumber);
                Console.WriteLine("Thao tác thành công");
            }
            else
            {
                Console.WriteLine("Tài khoản hiện đang không bị khóa hoặc không tồn tại");
            }
        }

        public void SearchTransactionHistory()
        {
            Console.WriteLine("Vui lòng nhập số tài khoản cần xem lịch sử giao dịch: ");
            var accountNumber = Console.ReadLine();
            var account = _accountModel.FindByAccountNumber(accountNumber);
            if (account != null)
            {
                var list =_accountModel.FindAllTransactionHistoriesByAccountNumber(accountNumber);
                if (list != null)
                {
                    Console.WriteLine(String.Format("|{0,37}|{1,37}|{2,37}|{3,4}|{4,10}|{5,15}|{6,22}|{7,6}|", "ID", "Sender Account Number", "Receiver Account Number", "Type", "Amount", "Message", "Created At", "Status"));
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    foreach (var t in list)
                    {
                        Console.WriteLine(t.ToString());
                    }    
                }
                else
                {
                    Console.WriteLine("Tài khoản hiện chưa có giao dịch.");
                }
            }
            else
            {
                Console.WriteLine("Số tài khoản không tồn tại.");
            }
        }

        public void UpdateInformation(Admin admin)
        {
            var isValid = false;
            do
            {
                admin = GetUpdateInformation(admin);
                var errors = admin.CheckValidUpdate();
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

            if (_adminModel.Update(admin))
            {
                Console.WriteLine("Cập nhật thông tin thành công.");
            }
        }
        
        private Admin GetUpdateInformation(Admin admin)
        {
            var isNotValidDob = true;
            Console.WriteLine("Vui lòng nhập tên của bạn: ");
            admin.FirstName = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập họ và tên đệm của bạn: ");
            admin.LastName = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập ngày sinh theo format mm/dd/yyyy");
            while (isNotValidDob)
            {
                try
                {
                    var inputDob = Console.ReadLine();
                    admin.Dob = DateTime.ParseExact(inputDob, "d", CultureInfo.InvariantCulture);
                    isNotValidDob = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Vui lòng nhập lại đúng format mm/dd/yyyy.");
                }
            }

            Console.WriteLine("Vui lòng nhập email: ");
            admin.Email = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập số điện thoại: ");
            admin.Phone = Console.ReadLine();
            Console.WriteLine("Vui lòng nhập địa chỉ");
            admin.Address = Console.ReadLine();
            return admin;
        }

        public void ChangePassword(Admin admin)
        {
            Console.WriteLine("Vui lòng nhập password hiện tại: ");
            var currentPassword = Console.ReadLine();
            if (Hash.ComparePassword(currentPassword, admin.Salt, admin.PasswordHash))
            {
                Console.WriteLine("Vui lòng nhập mật khẩu mới: ");
                var newPassword = Console.ReadLine();
                Console.Write("Vui lòng xác nhận mật khẩu: ");
                var newConfirmPassword = Console.ReadLine();
                if (newPassword == newConfirmPassword)
                {
                    admin.Password = newPassword;
                    admin.Salt = Hash.RandomString(7);
                    admin.PasswordHash = Hash.GenerateSaltedSHA1(admin.Password, admin.Salt);
                    if (_adminModel.UpdatePassword(admin))
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
    }
}