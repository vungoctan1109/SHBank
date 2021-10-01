using System;
using SHBank.Controller;

namespace SHBank.View
{
    public class SHBankMenu
    {
        private IAdminController _adminController;
        private IUserController _userController;

        public SHBankMenu()
        {
            _adminController = new AdminController();
            _userController = new UserController();
        }

        public void GenerateMenu()
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
                        Console.WriteLine("Tài khoản: ");
                        Console.WriteLine("1.1. Admin");
                        Console.WriteLine("1.2. User");
                        Console.WriteLine("Vui lòng nhập lựa chọn của bạn (1 hoặc 2)");
                        var choice2 = Convert.ToInt32(Console.ReadLine());
                        switch (choice2)
                        {
                            case 1:
                                _adminController.Register();
                                break;
                            case 2:
                                _userController.Register();
                                break;
                            default:
                                Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("Tài khoản: ");
                        Console.WriteLine("1.1. Admin");
                        Console.WriteLine("1.2. User");
                        Console.WriteLine("Vui lòng nhập lựa chọn của bạn (1 hoặc 2)");
                        var choice3 = Convert.ToInt32(Console.ReadLine());
                        switch (choice3)
                        {
                            case 1:
                                _adminController.Login();
                                break;
                            case 2:
                                _userController.Login();
                                break;
                            default:
                                Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                                break;
                        }
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