using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDLL
{
    public class Password
    {
        /// <summary>
        /// На вход подаем зашифрованный пароль, на выходе получаем пароль для email
        /// </summary>
        public static string Decrypt(string crypt)
        {
            string output = string.Empty;

            foreach (char a in crypt)
            {
                char ch = a;
                ch--;
                output += ch;
            }

            return output;
        }

        /// <summary>
        /// На вход подаем пароль, на выходе получаем зашифрованный пароль
        /// </summary>
        /// <param name="p_sPassword"></param>
        /// <returns></returns>
        public static string Encrypt(string password)
        {
            string output = string.Empty;

            foreach (char a in password)
            {
                char ch = a;
                ch++;
                output += ch;
            }

            return output;
        }

    }
}
