using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DiscordMikrotikBot.Extensions;

namespace DiscordMikrotikBot.Tools
{
    public class HashDatAss
    {
        // Hash: 56VgG33y2nJaQg7qCvZTcLlG3C7I0bL4t27xrsgG5M3EyvFDHjmeee7tUpnd6uy1LQQYkJ9kpsYASQhH8DbZYGYo5MNbedLKnb4SOQt8lbZWpGKdiQfCsIbe3B5I77gmRnmRXy3rjdOXRb0ybJ4y8jrP0Y1mVVbo6Mect4qTXXsp5ow1CGGwdRoI6HR9IPFybz2e87ehsjKj1cUVB0TpoOloyGgWmrlis4oFwr4GBze4geR4W6HI6qkwnNQAZr6b
        // Salt: W6OQgl6J6RzkD7JdEJgHI3lcBj8QiTrAMJa46HvK3pamNzOcnSsDlNW5TLp7ooAmA0I46Ph7nGPt29zbF5CzAYo9XgjtszwNYYWO2UhIKvpP8DcVmnBtWaVLVJgZFYOXkbEdlUkUX5ZfIIbtdyge9c2Xcl3rOrcWRCF790JBcRG2lQcmIWVgRJZhjQaa33fG2cxgdeveZPcqG0MwiQT2F7ehMxWs9IJkLaajs7iDBRrcHILkeN3mioa68pDQscfq
        // Vl: @psSvEMXYTwlneYv
        private string hash = "56VgG33y2nJaQg7qCvZTcLlG3C7I0bL4t27xrsgG5M3EyvFDHjmeee7tUpnd6uy1LQQYkJ9kpsYASQhH8DbZYGYo5MNbedLKnb4SOQt8lbZWpGKdiQfCsIbe3B5I77gmRnmRXy3rjdOXRb0ybJ4y8jrP0Y1mVVbo6Mect4qTXXsp5ow1CGGwdRoI6HR9IPFybz2e87ehsjKj1cUVB0TpoOloyGgWmrlis4oFwr4GBze4geR4W6HI6qkwnNQAZr6b";
        private string salt = "W6OQgl6J6RzkD7JdEJgHI3lcBj8QiTrAMJa46HvK3pamNzOcnSsDlNW5TLp7ooAmA0I46Ph7nGPt29zbF5CzAYo9XgjtszwNYYWO2UhIKvpP8DcVmnBtWaVLVJgZFYOXkbEdlUkUX5ZfIIbtdyge9c2Xcl3rOrcWRCF790JBcRG2lQcmIWVgRJZhjQaa33fG2cxgdeveZPcqG0MwiQT2F7ehMxWs9IJkLaajs7iDBRrcHILkeN3mioa68pDQscfq";
        private string vl = "@psSvEMXYTwlneYv";

        public string GetEncrypted(string plainTextBytes)
        {
            byte[] raw = Encoding.ASCII.GetBytes(plainTextBytes);
            byte[] keyBytes = new Rfc2898DeriveBytes(hash, Encoding.ASCII.GetBytes(salt)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(vl));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(raw, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }


        public string GetDecrypted(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(hash, Encoding.ASCII.GetBytes(salt)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(vl));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }
}
