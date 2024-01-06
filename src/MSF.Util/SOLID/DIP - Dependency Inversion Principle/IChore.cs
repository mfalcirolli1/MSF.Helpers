namespace MSF.Util.SOLID.DIP___Dependency_Inversion_Principle
{
    public interface IChore
    {
        string ChoreName { get; set; }
        double HoursWorked { get; set; }
        bool IsComplete { get; set; }
        IPerson Owner { get; set; }

        void CompleteChore();
        void PerformWork(double hours);
    }
}