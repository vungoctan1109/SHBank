using System;
using SHBank.Controller;
using SHBank.Entity;

namespace SHBank.View
{
    public class UserView
    {
        private IUserController _userController;

        public UserView()
        {
            _userController = new UserController();
        }

        public void GenerateUserView(Account account)
        {
            while (true)
            {
                Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                Console.WriteLine("Chào mừng “{0}” quay trở lại. Vui lòng chọn thao tác.", account.FirstName);
                Console.WriteLine("1. Gửi tiền.");
                Console.WriteLine("2. Rút tiền.");
                Console.WriteLine("3. Chuyển khoản.");
                Console.WriteLine("4. Truy vấn số dư.");
                Console.WriteLine("5. Thay đổi thông tin cá nhân.");
                Console.WriteLine("6. Thay đổi thông tin mật khẩu.");
                Console.WriteLine("7. Truy vấn lịch sử giao dịch.");
                Console.WriteLine("8. Thoát.");
                Console.WriteLine("—————————————————————————————————");
                Console.WriteLine("Nhập lựa chọn của bạn (Từ 1 đến 8): ");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        _userController.Deposit(account);
                        break;
                    case 2:
                        _userController.Withdraw(account);
                        break;
                    case 3:
                        _userController.Transfer(account);
                        break;
                    case 4:
                        _userController.CheckBalance(account);
                        break;
                    case 5:
                        _userController.UpdateInformation(account);
                        break;
                    case 6:
                        _userController.ChangePassword(account);
                        break;
                    case 7:
                        _userController.CheckTransactionHistory(account);
                        break;
                    case 8:
                        Console.WriteLine("Cảm ơn quý khách đã sử dụng dịch vụ của SH Bank.");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Mời quý khách chọn lại.");
                        break;
                }

                if (choice == 8)
                {
                    break;
                }

                Console.ReadLine();
            }
        }
    }
}