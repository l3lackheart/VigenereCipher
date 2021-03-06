﻿using System;
using System.Text;

namespace VingenereCipher
{
    class VingenereCipher
    {
        private int rawLength { get; set; }
        private string key = "";
        public string keyword {
            get { return key; }
            set
            {
                key = value;
                // set default value for keyword
                if (value == "") key = "STILLALIVE";
            }
        }

        // Remove characters not in Z26 alphabet
        public string RemoveNonZ26(string raw)
        {
            raw = raw.ToUpper();
            do
            {
                int i = 0;
                for (; i < raw.Length; i++)
                {
                    int ch = (int)raw[i];
                    if (ch < 65 || ch > 90)
                    {
                        raw = raw.Remove(i, 1);
                        break;
                    }
                }
                if (i == raw.Length)
                {
                    break;
                }
            }
            while (true);

            return raw;

        }

        public string Encrypt(string raw, string keyword)
        {
            raw = RemoveNonZ26(raw);
            keyword = RemoveNonZ26(keyword);
            rawLength = raw.Length;

            // fill length
            if (raw.Length > keyword.Length)
            {
                int smallerLength = keyword.Length;
                for (int i = keyword.Length; i < raw.Length; i++)
                {
                    keyword += keyword[(i % smallerLength)];
                }
            }
            else
            {
                for (int i = raw.Length; i < keyword.Length; i++)
                {
                    int smallerLength = raw.Length;
                    raw += raw[(i % smallerLength)];
                }
            }
            
            // bind string into an array of bytes
            byte[] rawData = Encoding.ASCII.GetBytes(raw);
            byte[] keyData = Encoding.ASCII.GetBytes(keyword);

            for (int index = 0; index < rawData.Length; index++)
            {
                byte rawToken = (byte)(rawData[index] - 65); // minus by 65 to back to Z26 context
                byte keyToken = (byte)(keyData[index] - 65); // minus by 65 to back to Z26 context

                byte sumToken = (byte)(rawToken + keyToken);

                if (sumToken > 25)
                    sumToken -= 26;
                sumToken += 65; // plus by 65 to back to ASCII character


                rawData[index] = sumToken;
            }

            // bind array into a string
            string result = Encoding.ASCII.GetString(rawData);

            return result;
        }

        public string Decrypt(string encrypted, string keyword)
        {
            encrypted = RemoveNonZ26(encrypted);
            keyword = RemoveNonZ26(keyword);

            // fill length
            if (encrypted.Length > keyword.Length)
            {
                int smallerLength = keyword.Length;
                for (int i = keyword.Length; i < encrypted.Length; i++)
                {
                    keyword += keyword[(i % smallerLength)];
                }
            }
            else
            {
                for (int i = encrypted.Length; i < keyword.Length; i++)
                {
                    int smallerLength = encrypted.Length;
                    encrypted += encrypted[(i % smallerLength)];
                }
            }

            // bind string into an array of bytes
            byte[] encryptedData = Encoding.ASCII.GetBytes(encrypted);
            byte[] keyData = Encoding.ASCII.GetBytes(keyword);


            for (int index = 0; index < rawLength; index++)
            {
                byte encryptedToken = (byte)(encryptedData[index] - 65); // minus by 65 to back to Z26 context
                byte keyToken = (byte)(keyData[index] - 65); // minus by 65 to back to Z26 context

                int sumToken = (encryptedToken - keyToken);

                if (sumToken < 0)
                    sumToken += 26;

                sumToken += 65; // plus by 65 to back to ASCII character


                encryptedData[index] = (byte)sumToken;
            }

            // bind array into a string
            string result = Encoding.ASCII.GetString(encryptedData);

            if (rawLength < keyword.Length)
            {
                result = result.Remove(rawLength);
            }
            return result;
        }
    }
}
