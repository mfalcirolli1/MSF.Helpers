using System;
using MSF.ChatGPT.ChatGPT;
using MSF.Util.Base64;

namespace MSF.Util.Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string apiKey = Base64Extension.TransformBase64ToText("c2stMGxxVno2dmpRYmNtZ3BES0FmRW9UM0JsYmtGSlJES2FzN3g3WWRsWkQxeHduN2Z3");
            var chatGPTClient = new ChatGPTClient(apiKey);

            Console.WriteLine("Welcome to the ChatGPT chatbot! Type 'exit' to quit.");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("You: ");
                Console.ResetColor();
                string input = Console.ReadLine() ?? string.Empty;

                if (input.ToLower() == "exit")
                    break;

                string response = chatGPTClient.SendMessage(input);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Chatbot: ");
                Console.ResetColor();
                Console.WriteLine(response);

                Console.WriteLine();
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine();
            }
        }
    }
}
