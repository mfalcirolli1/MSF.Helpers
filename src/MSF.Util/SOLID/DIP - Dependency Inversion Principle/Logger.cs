﻿using System.Diagnostics;

namespace MSF.Util.SOLID.DIP___Dependency_Inversion_Principle
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine($"- Log: {message}");
        }
    }
}
