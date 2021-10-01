using System;
using System.Collections.Generic;
using SHBank.Util;

namespace SHBank.Entity
{
    public class Account
    {
        public string AccountNumber { get; set; }
        
        public double Balance { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPassword { get; set; }
        public string Salt { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string IdentityNumber { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int Status { get; set; }

        public Account()
        {
            AccountNumber = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            DeletedAt = DateTime.Now;
            Status = 1;
        }

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

            if (this.Balance < 0)
            {
                errors.Add("balance", "Số tiền không hợp lệ. Vui lòng nhập lại số tiền muốn gửi vào ngân hàng.");
            }
            if (string.IsNullOrEmpty(this.FirstName))
            {
                errors.Add("username", " Không được để trống tên.");
            }
            
            if (string.IsNullOrEmpty(this.LastName))
            {
                errors.Add("username", "Không được để trống họ và tên đệm.");
            }

            if (this.Gender < 1 && this.Gender > 2 )
            {
                errors.Add("gender", "Mời quý khách chọn lại.");
            }
            
            if (string.IsNullOrEmpty(this.Email))
            {
                errors.Add("email", "Không được để trống email.");
            }
            
            if (string.IsNullOrEmpty(this.IdentityNumber))
            {
                errors.Add("identityNumber", "Không được để trống số cmnd/cccd .");
            }
            
            if (string.IsNullOrEmpty(this.Phone))
            {
                errors.Add("phone", "Không được để trống số điện thoai.");
            }
            
            if (string.IsNullOrEmpty(this.Address))
            {
                errors.Add("address", "Không được để trống địa chỉ.");
            }
            return errors;
        }
        
        public Dictionary<string, string> CheckValidLogin()
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

            return errors;
        }

        public void EncryptPassword()
        {
            Salt = Hash.RandomString(7);
            PasswordHash = Hash.GenerateSaltedSHA1(Password, Salt);
        }

        public void GenerateAccountNumber()
        {
            AccountNumber = Guid.NewGuid().ToString();
        }
        
    }
}