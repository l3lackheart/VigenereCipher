﻿using System;

namespace VingenereCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            VingenereCipher VingenereCipher = new VingenereCipher();
            string plainText;
            Console.Write("Nhap van ban can ma hoa: ");
            plainText = Console.ReadLine();

            Console.Write("Nhap vao tu khoa, bo trong neu muon dung tu khoa mac dinh: ");
            VingenereCipher.keyword = Console.ReadLine();


            string encrypted = VingenereCipher.Encrypt(plainText, VingenereCipher.keyword);
            Console.WriteLine("Van ban da ma hoa la:\n{0}", encrypted);

            string decrypted = VingenereCipher.Decrypt(encrypted, VingenereCipher.keyword);
            Console.WriteLine("Van ban da giai ma la:\n{0}", decrypted);

            Console.ReadKey();
        }
    }
}
