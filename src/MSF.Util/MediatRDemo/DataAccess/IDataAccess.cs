using MSF.Util.MediatRDemo.Models;
using System.Collections.Generic;

namespace MSF.Util.MediatRDemo.DataAccess
{
    public interface IDataAccess
    {
        List<PersonModel> GetPeople();
        PersonModel InsertPerson(string firstName, string lastName);
    }
}