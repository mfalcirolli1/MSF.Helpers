using MSF.Util.Core.FluentEmail;
using MSF.Util.Generics;
using System;
using System.Threading.Tasks;

namespace MSF.Util.Core
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var esender = new EmailSender();
            // await esender.SendEmail();

            var generic = new Generics.Generics();
            var teste = generic.ReadFile<ExModel>("C:\\Users\\Falt_\\Documentos\\github\\Log\\Generic.txt");
        }
    }
}
