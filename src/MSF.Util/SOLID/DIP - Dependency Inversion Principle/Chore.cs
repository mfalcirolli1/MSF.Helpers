using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.SOLID.DIP___Dependency_Inversion_Principle
{
    // High-level modules should not depend on low-level modules,
    // but rather both should depend on abstractions,
    // and those abstractions should not depend on details

    // Dependency Inversion is the Principle
    // Dependency Injection is one of the ways that make the Principle work

    public class Chore : IChore
    {
        public string ChoreName { get; set; }
        public IPerson Owner { get; set; }
        public double HoursWorked { get; set; } // Double = 64 bits (8 bytes) | Decimal = 128 bits (16 bytes)
        public bool IsComplete { get; set; }

        ILogger _logger;
        IMessageSender _messageSender;

        public Chore(ILogger logger, IMessageSender messageSender)
        {
            _logger = logger;
            _messageSender = messageSender;
        }

        public void PerformWork(double hours)
        {
            HoursWorked += hours;
            // Logger log = new Logger();
            _logger.Log($"Perform work on {ChoreName}");
        }

        public void CompleteChore()
        {
            IsComplete = true;

            //Logger log = new Logger();
            _logger.Log($"Completed {ChoreName}");

            //Emailer emailer = new Emailer();
            _messageSender.SendMessage(Owner, $"The chore {ChoreName} is complete.");
        }
    }
}
