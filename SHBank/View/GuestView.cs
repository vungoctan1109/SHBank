using System;

namespace SHBank.View
{
    public class GuestView
    {
        public void GenerateGuestView()
        {
            while (true)
            {
                Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                Console.WriteLine("1. Đăng kí tài khoản.");
                Console.WriteLine("2. Đăng nhập hệ thống.");
                Console.WriteLine("3. Thoát.");
                Console.WriteLine("—————————————————————————————————");
                Console.WriteLine("Nhập lựa chọn của bạn (1,2,3): ");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        Console.WriteLine("Cảm ơn quý khách đã sử dụng dịch vụ của SH Bank.");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Mời quý khách chọn lại.");
                        break;
                }

                if (choice == 3)
                {
                    break;
                }

                Console.ReadLine();
            }
        }
    }
}