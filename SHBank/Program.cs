
using System;
using SHBank.Controller;
using SHBank.View;

namespace SHBank
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            SHBankMenu shBankMenu = new SHBankMenu();
            shBankMenu.GenerateMenu();
        }
    }
}