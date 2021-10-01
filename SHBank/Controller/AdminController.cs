using System;
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

        public AdminController()
        {
            _adminModel = new AdminModel();
        }
        public Admin Register()
        {
            Admin admin = null;
            var isValid = false;
            do
            {
                admin = GetAccountInformation();
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

        private Admin GetAccountInformation()
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
            throw new System.NotImplementedException();
        }

        public void ShowListTransaction()
        {
            throw new System.NotImplementedException();
        }

        public void FindUserByAccountNumber()
        {
            throw new System.NotImplementedException();
        }

        public void FindUserByName()
        {
            throw new System.NotImplementedException();
        }

        public void FindUserByPhone()
        {
            throw new System.NotImplementedException();
        }

        public void CreateNewUser()
        {
            throw new System.NotImplementedException();
        }

        public void LockUser()
        {
            throw new System.NotImplementedException();
        }

        public void UnlockUser()
        {
            throw new System.NotImplementedException();
        }

        public void SearchTransactionHistory()
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
    }
}