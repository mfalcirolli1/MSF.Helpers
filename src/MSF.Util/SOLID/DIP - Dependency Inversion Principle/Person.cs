using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.SOLID.DIP___Dependency_Inversion_Principle
{
    public class Person : IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
