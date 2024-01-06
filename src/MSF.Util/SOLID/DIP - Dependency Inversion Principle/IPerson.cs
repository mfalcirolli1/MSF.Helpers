namespace MSF.Util.SOLID.DIP___Dependency_Inversion_Principle
{
    public interface IPerson
    {
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
    }
}