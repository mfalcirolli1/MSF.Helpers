using MSF.Util.SOLID.DIP___Dependency_Inversion_Principle;

namespace MSF.UnitTests.DIP
{
    public class ProgramDemo
    {
        public void Execute()
        {
            IPerson owner = Factory.CreatePerson();
            owner.EmailAddress = "email@email.com.br";
            owner.FirstName = "First";
            owner.LastName = "Last";
            owner.PhoneNumber = "11 98888 7777";

            //IPerson owner = new Person()
            //{
            //    EmailAddress = "email@email.com.br",
            //    FirstName = "First",
            //    LastName = "Last",
            //    PhoneNumber = "11 98888 7777"
            //};

            IChore chore = Factory.CreateChore();
            chore.ChoreName = "Take out the trash";
            chore.Owner = owner;

            //Chore chore = new Chore()
            //{
            //    ChoreName = "Take out the trash",
            //    Owner = owner,
            //};

            chore.PerformWork(3);
            chore.PerformWork(1.5);
            chore.CompleteChore();
        }
    }
}
