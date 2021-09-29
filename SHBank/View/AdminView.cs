using System;

namespace SHBank.View
{
    public class AdminView
    {
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
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
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