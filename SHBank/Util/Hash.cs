﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SHBank.Util
{
    public class Hash
    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine(GenerateSaltedSHA1("123456", "EQQ9YSY"));         
//        }
        
        private static Random random = new Random();
        
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        public static string GenerateSaltedSHA1(string passwordString, string salt)
        {
            HashAlgorithm algorithm = new SHA1Managed(); // Thuật toán mã hoá.            
            var saltBytes = Encoding.ASCII.GetBytes(salt); // Tạo muối và chuyển đổi muối về dạng byte array.
            var plainTextBytes = Encoding.ASCII.GetBytes(passwordString); // Chuyển đổi password về dạng byte array.

            var plainTextWithSaltBytes = AppendByteArray(plainTextBytes, saltBytes); // Ghép 2 byte array của muối và password vào.
            var saltedSHA1Bytes = algorithm.ComputeHash(plainTextWithSaltBytes); // mã hoá mảng các byte được tạo ra.
           
            return Convert.ToBase64String(saltedSHA1Bytes); // Chuyển đổi mạng về dạng string.
        } 
        
        private static byte[] GenerateSalt(int saltSize)
        {
            var rng = new RNGCryptoServiceProvider(); // Tạo ra số ngẫu nhiên.
            var buff = new byte[saltSize]; // Tạo ra một mảng chứa các bytes theo size truyền vào trong tham số.
            rng.GetBytes(buff);
            return buff; 
        }
        
        private static byte[] AppendByteArray(byte[] byteArray1, byte[] byteArray2)
        {
            var byteArrayResult =
                new byte[byteArray1.Length + byteArray2.Length];

            for (var i = 0; i < byteArray1.Length; i++)
                byteArrayResult[i] = byteArray1[i];
            for (var i = 0; i < byteArray2.Length; i++)
                byteArrayResult[byteArray1.Length + i] = byteArray2[i];

            return byteArrayResult;
        }

        public static bool ComparePassword(string input, string salt, string passwordHash)
        {
            return GenerateSaltedSHA1(input, salt).Equals(passwordHash);
        }

    }
}