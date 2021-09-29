using System;

namespace SHBank.View
{
    public class UserView
    {
        public void GenerateUserView()
        {
            while (true)
            {
                Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                Console.WriteLine("Chào mừng “Xuân Hùng” quay trở lại. Vui lòng chọn thao tác.");
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