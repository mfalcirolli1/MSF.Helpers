using System.Diagnostics;

namespace MSF.Util.SOLID.DIP___Dependency_Inversion_Principle
{
    public class Emailer
    {
        public void SendEmail(Person person, string message)
        {
            Debug.WriteLine($"Sending email to: {person.EmailAddress}");
        }
    }
}
