using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.MediatRDemo.Models
{
    public class PersonModel
    {
        // CQRS -> Command Query Responsibility Segregation
        // CRUD -> R (Get data) | CUD (Update data)

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
