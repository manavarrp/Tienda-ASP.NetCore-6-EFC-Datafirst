using SALESSYSTEM.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SALESSYSTEM.BLL.Implementation
{
    public class UtilitiesServices : IUtilitiesService
    {

        public string CreatePassword()
        {
            string password = Guid.NewGuid().ToString("N").Substring(0,6);
            return password;
        }

        //metodo para encriptar la clave
        public string ConvertSha256(string text)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(text));

                foreach (byte b in result)
                {
                    stringBuilder.Append(b.ToString("X2"));
                }
            }

            return stringBuilder.ToString();
        }

       
    }
}
