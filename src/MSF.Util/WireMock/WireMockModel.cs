namespace MSF.Util.WireMock
{
    public class WireMockModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string Reference { get; set; }
        public string CEP { get; set; }
    }
}
