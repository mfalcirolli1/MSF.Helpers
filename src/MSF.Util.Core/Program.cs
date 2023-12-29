﻿using BenchmarkDotNet.Running;
using MSF.Util.Bogus;
using MSF.Util.Collections;
using MSF.Util.Core.FluentEmail;
using MSF.Util.EPPlus;
using MSF.Util.FluentValidation;
using MSF.Util.Generics;
using MSF.Util.Humanizer;
using MSF.Util.Mapper;
using MSF.Util.Markdig;
using MSF.Util.Polly;
using MSF.Util.Singleton_vs_Static;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSF.Util.Tasks;
using MSF.Util.LazyLoad;
using MSF.Util.Asynchronous;
using MSF.Util.SecurePassword;
using MSF.ChatGPT.ChatGPT;

namespace MSF.Util.Core
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string apiKey = "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";
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
