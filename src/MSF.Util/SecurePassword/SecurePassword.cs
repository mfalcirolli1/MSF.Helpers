using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace MSF.Util.SecurePassword
{
    public static class SecurePasswordDemo
    {
        public static void Teste()
        {
            var password = new SecureString();
            Console.WriteLine("Digite a senha: ");
            ObterSenha(password);
        }

        private static void ObterSenha(SecureString password)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            while (keyInfo.Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.RemoveAt(password.Length - 1);
                        Console.Write(keyInfo.KeyChar);
                        Console.Write(" ");
                        Console.Write(keyInfo.KeyChar);
                    }
                }
                else
                {
                    password.AppendChar(keyInfo.KeyChar);
                    Console.Write("*");
                }
                keyInfo = Console.ReadKey(true);
            }
            password.MakeReadOnly();
        }
    }
}
