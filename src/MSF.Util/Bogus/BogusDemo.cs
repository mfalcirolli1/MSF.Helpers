using Bogus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Util.Bogus
{
    // Generates Fake Data for tests

    public static class BogusDemo
    {
        public static List<Customer> GenerateCustomer()
        {
            var customerGenerator = new Faker<Customer>()
                .RuleFor(x => x.Name, f => f.Name.FullName())
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.IPAddress, f => f.Internet.Ip())
                .RuleFor(x => x.Music, f => f.Music.Genre())
                .RuleFor(x => x.Telefone, f => f.Phone.PhoneNumber())
                .RuleFor(x => x.Cartao, f => f.Finance.CreditCardNumber()
                );

            return customerGenerator.Generate(3);
        }
    }

    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cartao { get; set; }
        public string IPAddress { get; set; }
        public string Music { get; set; }
    }
}
