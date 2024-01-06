namespace MSF.Util.SOLID.DIP___Dependency_Inversion_Principle
{
    public interface IMessageSender
    {
        void SendMessage(IPerson person, string message);
    }
}