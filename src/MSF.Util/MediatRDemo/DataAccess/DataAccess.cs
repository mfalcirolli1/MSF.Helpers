using MSF.Util.MediatRDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSF.Util.MediatRDemo.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private List<PersonModel> people = new List<PersonModel>();

        public DataAccess()
        {
            people.Add(new PersonModel() { Id = 1, FirstName = "FN1", LastName = "LM1" });
            people.Add(new PersonModel() { Id = 2, FirstName = "FN2", LastName = "LM2" });
        }

        public List<PersonModel> GetPeople()
        {
            return people;
        }

        public PersonModel InsertPerson(string firstName, string lastName)
        {
            var person = new PersonModel
            {
                Id = people.Max(x => x.Id) + 1,
                FirstName = firstName,
                LastName = lastName
            };
            people.Add(person);

            return person;
        }
    }
}
