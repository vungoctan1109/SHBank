using System.Collections.Generic;
using SHBank.Util;

namespace SHBank.Entity
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPassword { get; set; }
        public string Salt { get; set; }

        public Dictionary<string, string> IsValid()
        {
            var errors = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(this.Username))
            {
                errors.Add("username", "Username không được để trống.");
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                errors.Add("username", "Password không được để trống.");
            }

            if (!this.Password.Equals(this.ConfirmPassword))
            {
                errors.Add("confirmPassword", "Mật khẩu và xác nhận mật khẩu không trùng nhau.");
            }
            return errors;
        }

        public void EncryptPassword()
        {
            Salt = Hash.RandomString(5);
            PasswordHash = Hash.GenerateSaltedSHA1(Password, Salt);
        }
    }
}