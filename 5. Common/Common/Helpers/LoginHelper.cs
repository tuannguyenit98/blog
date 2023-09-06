using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers
{
    public static class LoginHelper
    {
        public static string EncryptPassword(string passsword)
        {
            StringBuilder hash = new StringBuilder();
            byte[] bytes = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(passsword));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static bool CheckPassword(string passwordToVerify, string encryptedPassword)
        {
            return EncryptPassword(passwordToVerify).Equals(encryptedPassword, StringComparison.InvariantCulture);
        }       
    }
}
