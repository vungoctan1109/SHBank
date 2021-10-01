using System;
using SHBank.Controller;

namespace SHBank.View
{
    public class AdminView
    {
        private IAdminController _adminController;

        public AdminView()
        {
            _adminController = new AdminController();
        }

        public void GenerateAdminView()
        {
            while (true)
            {
                Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                Console.WriteLine("Chào mừng Admin “Xuân Hùng” quay trở lại. Vui lòng chọn thao tác.");
                Console.WriteLine("1. Danh sách người sử dụng.");
                Console.WriteLine("2. Danh sách lịch sử giao dịch.");
                Console.WriteLine("3. Tìm kiếm người dùng theo tên.");
                Console.WriteLine("4. Tìm kiếm người dùng theo số tài khoản.");
                Console.WriteLine("5. Tìm kiếm người dùng theo số điện thoại.");
                Console.WriteLine("6. Thêm người dùng mới.");
                Console.WriteLine("7. Khóa và mở tài khoản người dùng.");
                Console.WriteLine("8. Tìm kiếm lịch sử giao dịch theo số tài khoản.");
                Console.WriteLine("9. Thay đổi thông tin tài khoản.");
                Console.WriteLine("10. Thay đổi thông tin mật khẩu.");
                Console.WriteLine("11. Thoát.");
                Console.WriteLine("—————————————————————————————————");
                Console.WriteLine("Nhập lựa chọn của bạn (Từ 1 đến 11): ");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        _adminController.ShowListUser();
                        break;
                    case 2:
                        _adminController.ShowListTransaction();
                        break;
                    case 3:
                        _adminController.FindUserByName();
                        break;
                    case 4:
                        _adminController.FindUserByPhone();
                        break;
                    case 5:
                        _adminController.FindUserByAccountNumber();
                        break;
                    case 6:
                        _adminController.CreateNewUser();
                        break;
                    case 7:
                        Console.WriteLine("Lựa chọn của bạn là: ");
                        Console.WriteLine("7.1. Khóa tài khoản ");
                        Console.WriteLine("7.2. Mở khóa tài khoản");
                        Console.WriteLine("Nhập lựa chọn của bạn (1 hoặc 2): ");
                        var choice2 = Convert.ToInt32(Console.ReadLine());
                        switch (choice2)
                        {
                            case 1:
                                _adminController.LockUser();
                                break;
                            case 2:
                                _adminController.UnlockUser();
                                break;
                            default:
                                Console.WriteLine("Vui lòng nhập lại lựa chọn của bạn.");
                                break;
                        }

                        break;
                    case 8:
                        _adminController.SearchTransactionHistory();
                        break;
                    case 9:
                        _adminController.UpdateInformation();
                        break;
                    case 10:
                        _adminController.ChangePassword();
                        break;
                    case 11:
                        Console.WriteLine("Đã chấm dứt phiên làm việc.");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Hãy chọn lại.");
                        break;
                }

                if (choice == 11)
                {
                    break;
                }

                Console.ReadLine();
            }
        }
    }
}