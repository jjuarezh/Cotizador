using System;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Utils
{
    public class Cryptography
    {
        public static string IV = @"9QAZ2WSX5EDC4RFV";
        public static string Key = @"5TGB&YHN7UJM(IK<5TGB&YHN7UJM(IK<";

        public static string EncryptAES(string plainText)
        {
            byte[] textBytes = Encoding.ASCII.GetBytes(plainText);
            AesCryptoServiceProvider encdec = new AesCryptoServiceProvider();
            encdec.BlockSize = 128;
            encdec.KeySize = 256;
            encdec.Key = Encoding.UTF8.GetBytes(Key);
            encdec.IV = Encoding.UTF8.GetBytes(IV);
            encdec.Padding = PaddingMode.PKCS7;
            encdec.Mode = CipherMode.CBC;

            ICryptoTransform icrypt = encdec.CreateEncryptor(encdec.Key, encdec.IV);
            byte[] enc = icrypt.TransformFinalBlock(textBytes, 0, textBytes.Length);
            icrypt.Dispose();

            return Convert.ToBase64String(enc);
        }

        public static string DecryptAES(string encrypted)
        {
            byte[] encBytes = Convert.FromBase64String(encrypted);
            AesCryptoServiceProvider encdec = new AesCryptoServiceProvider();
            encdec.BlockSize = 128;
            encdec.KeySize = 256;
            encdec.Key = Encoding.UTF8.GetBytes(Key);
            encdec.IV = Encoding.UTF8.GetBytes(IV);
            encdec.Padding = PaddingMode.PKCS7;
            encdec.Mode = CipherMode.CBC;

            ICryptoTransform icrypt = encdec.CreateDecryptor(encdec.Key, encdec.IV);
            byte[] dec = icrypt.TransformFinalBlock(encBytes, 0, encBytes.Length);
            icrypt.Dispose();

            return Encoding.ASCII.GetString(dec);
        }
    }
}
