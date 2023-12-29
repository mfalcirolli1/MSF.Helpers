using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.SOLID.DIP___Dependency_Inversion_Principle
{
    public class Chore
    {
        public string ChoreName { get; set; }
        public Person Owner { get; set; }
        public double HoursWorked { get; set; } // Double = 64 bits (8 bytes) | Decimal = 128 bits (16 bytes)
        public bool IsComplete { get; set; }

        public void PerformWork(double hours)
        {
            HoursWorked += hours;
            Logger log = new Logger();
            log.Log($"Perform work on {ChoreName}");
        }

        public void CompleteChore()
        {
            IsComplete = true;

            Logger log = new Logger();
            log.Log($"Completed {ChoreName}");

            Emailer emailer = new Emailer();
            emailer.SendEmail(Owner, $"The chore {ChoreName} is complete.");
        }

        public void Execute()
        {
            Person owner = new Person()
            {
                EmailAddress = "email@email.com.br",
                FirstName = "First",
                LastName = "Last",
                PhoneNumber = "11 98888 7777"
            };

            Chore chore = new Chore()
            {
                ChoreName = "Take out the trash",
                Owner = owner,
            };

            chore.PerformWork(3);
            chore.PerformWork(1.5);
            chore.CompleteChore();
        }
    }
}
