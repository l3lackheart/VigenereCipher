﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VingenereCipher
{
    class VingenereCipher
    {
        private string key { get; set; }

        public VingenereCipher()
        {
            key = "THISCRYPTOSYSTEMISNOTSECURE";
        }

        public string Encrypt(string raw)
        {
            string fillString = raw.ToUpper(); //chane text to upper case

            for (int i = fillString.Length; i < key.Length; i++)
            {
                fillString += fillString[(i % raw.Length)];
            }



            // bind string into an array of bytes
            byte[] fillData = Encoding.ASCII.GetBytes(fillString);
            byte[] keyData = Encoding.ASCII.GetBytes(key);

            for (int index = 0; index < fillString.Length; index++)
            {
                byte fillToken = (byte)(fillData[index] - 65); // minus by 65 to back to Alphabet code context
                byte keyToken = (byte)(keyData[index] - 65); // minus by 65 to back to Alphabet code context

                byte sumToken = (byte)(fillToken + keyToken);

                if (sumToken > 25)
                    sumToken -= 26;
                sumToken += 65; // plus by 65 to back to ASCII character


                fillData[index] = sumToken;
            }

            // bind array into a string
            string result = Encoding.ASCII.GetString(fillData);

            return result;
        }
    }
}
